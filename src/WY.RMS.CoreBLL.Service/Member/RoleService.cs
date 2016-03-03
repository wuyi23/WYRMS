using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using WY.RMS.Component.Data;
using WY.RMS.Component.Data.EF.Interface;
using WY.RMS.Component.Tools;
using WY.RMS.Domain.Data.Repositories.Member;
using WY.RMS.Domain.Model.Member;
using WY.RMS.ViewModel.Member;

namespace WY.RMS.CoreBLL.Service
{
    public class RoleService : CoreServiceBase, IRoleService
    {
        private readonly IRoleRepository _RoleRepository;

        public RoleService(IRoleRepository roleRepository, IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            this._RoleRepository = roleRepository;
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
            using (var scope = new TransactionScope())
            {
                try
                {
                    foreach (var item in list)
                    {
                        _RoleRepository.Delete(item.Id, false);
                    }
                    UnitOfWork.Commit();
                    scope.Complete();
                    return new OperationResult(OperationResultType.Success, "删除数据成功！");
                }
                catch
                {
                    return new OperationResult(OperationResultType.Error, "删除数据失败!");
                }
            }

        }
    }
}
