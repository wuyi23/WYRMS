using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Transactions;
using WY.RMS.Component.Data;
using WY.RMS.Component.Data.EF.Interface;
using WY.RMS.Component.Tools;
using WY.RMS.Domain.Data.Repositories.Member;
using WY.RMS.Domain.Model.Member;
using WY.RMS.ViewModel;
using WY.RMS.ViewModel.Member;

namespace WY.RMS.CoreBLL.Service
{
    public class RoleService : CoreServiceBase, IRoleService
    {
        private readonly IRoleRepository _RoleRepository;
        private readonly IModuleService _ModuleService;
        private readonly IPermissionService _PermissionService;

        public RoleService(IRoleRepository roleRepository, IModuleService moduleService, IPermissionService permissionService, IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            this._RoleRepository = roleRepository;
            this._ModuleService = moduleService;
            this._PermissionService = permissionService;
        }
        public IQueryable<Role> Roles
        {
            get { return _RoleRepository.Entities; }
        }

        public OperationResult Insert(RoleVM model)
        {
            try
            {
                Role oldRole = _RoleRepository.Entities.FirstOrDefault(c => c.RoleName == model.RoleName.Trim());
                if (oldRole != null)
                {
                    return new OperationResult(OperationResultType.Warning, "数据库中已经存在相同名称的角色，请修改后重新提交！");
                }
                var entity = new Role
                {
                    RoleName = model.RoleName.Trim(),
                    Description = model.Description,
                    OrderSort = model.OrderSort,
                    Enabled = model.Enabled,
                    UpdateDate = DateTime.Now
                };
                _RoleRepository.Insert(entity);
                return new OperationResult(OperationResultType.Success, "新增数据成功！");
            }
            catch
            {
                return new OperationResult(OperationResultType.Error, "新增数据失败，数据库插入数据时发生了错误!");
            }
        }
        public OperationResult Update(RoleVM model)
        {
            try
            {
                var oldRole = Roles.FirstOrDefault(c => c.Id == model.Id);
                if (oldRole == null)
                {
                    throw new Exception();
                }
                var other = Roles.FirstOrDefault(c => c.Id != model.Id && c.RoleName == model.RoleName.Trim());
                if (other != null)
                {
                    return new OperationResult(OperationResultType.Warning, "数据库中已经存在相同名称的角色，请修改后重新提交！");
                }
                oldRole.RoleName = model.RoleName.Trim();
                oldRole.Description = model.Description;
                oldRole.OrderSort = model.OrderSort;
                oldRole.Enabled = model.Enabled;
                oldRole.UpdateDate = DateTime.Now;
                _RoleRepository.Update(oldRole);
                return new OperationResult(OperationResultType.Success, "更新数据成功！");
            }
            catch
            {
                return new OperationResult(OperationResultType.Error, "更新数据失败!");
            }
        }

        public OperationResult Delete(IEnumerable<RoleVM> list)
        {
            try
            {
                using (var scope = new TransactionScope())
                {

                    foreach (var item in list)
                    {
                        _RoleRepository.Delete(item.Id, false);
                    }
                    UnitOfWork.Commit();
                    scope.Complete();
                    return new OperationResult(OperationResultType.Success, "删除数据成功！");

                }
            }
            catch
            {
                return new OperationResult(OperationResultType.Error, "删除数据失败!");
            }
        }
        /// <summary>
        /// 构造树的数据源
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList<ZTreeVM> GetListZTreeVM(int id)
        {
            List<ZTreeVM> result = new List<ZTreeVM>();
            List<double> permissionIds = Roles.First(c => c.Id == id).Permissions.Select(c => c.Id + 0.5).ToList();
            List<ZTreeVM> mouduleNodes = _ModuleService.Modules.Where(c => c.Enabled == true).OrderBy(c => c.Code).Select(c => new ZTreeVM()
            {
                id = c.Id,
                pId = c.ParentId,
                name = c.Name,
                isParent = !c.ParentId.HasValue,
                open = !c.ParentId.HasValue
            }).ToList();
            List<ZTreeVM> permissionNodes =
                _PermissionService.Permissions.Where(c => c.Enabled == true).Select(c => new ZTreeVM()
                {
                    id = c.Id + 0.5,
                    pId = c.ModuleId,
                    name = c.Name
                }).ToList();
            foreach (var node in permissionNodes)
            {
                if (permissionIds.Contains(node.id))
                {
                    node.@checked = true;
                }
            }
            result.AddRange(mouduleNodes);
            result.AddRange(permissionNodes);
            return result;
        }

        //更新权限授权
        public OperationResult UpdateAuthorize(int roleId, int[] ids)
        {
            try
            {
                using (var scope = new TransactionScope())
                {

                    var oldRole = Roles.FirstOrDefault(c => c.Id == roleId);
                    if (oldRole == null)
                    {
                        throw new Exception();
                    }
                    oldRole.Permissions.Clear();
                    var permissions = _PermissionService.Permissions.Where(c => ids.Contains(c.Id)).ToList();
                    oldRole.Permissions = permissions;
                    UnitOfWork.Commit();
                    scope.Complete();
                    return new OperationResult(OperationResultType.Success, "更新角色权限成功！");
                }
            }
            catch
            {
                return new OperationResult(OperationResultType.Error, "更新角色权限失败!");
            }
        }

    }
}
