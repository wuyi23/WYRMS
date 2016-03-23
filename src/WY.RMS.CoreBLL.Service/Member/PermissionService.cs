/************************************
 * 描述：尚未添加描述
 * 作者：吴毅
 * 日期：2015/11/20 15:16:38  
*************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using WY.RMS.Component.Data;
using WY.RMS.Component.Data.EF.Interface;
using WY.RMS.Component.Tools;
using WY.RMS.Domain.Data.Repositories.Member;
using WY.RMS.Domain.Model.Member;
using WY.RMS.ViewModel.Member;

namespace WY.RMS.CoreBLL.Service
{
    public class PermissionService : CoreServiceBase, IPermissionService
    {
        private readonly IPermissionRepository _PermissionRepository;

        public PermissionService(IPermissionRepository permissionRepository, IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            this._PermissionRepository = permissionRepository;
        }
        public IQueryable<Permission> Permissions
        {
            get
            {
                return _PermissionRepository.Entities;
            }
        }

        /// <summary>
        /// 获取模块分页列表
        /// </summary>
        /// <param name="wh">查询where表达式</param>
        /// <param name="limit"></param>
        /// <param name="offset"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public IList<PermissionVM> GetListPermissionVM(Expression<Func<Permission, bool>> wh, int limit, int offset, out int total)
        {
            return _PermissionRepository.GetListPermissionVM(wh, limit, offset, out total);
        }



        public OperationResult Insert(PermissionVM model)
        {
            try
            {
                Permission oldPermission = Permissions.Where(c => c.module.Id == model.ModuleId).FirstOrDefault(c => (c.Name == model.Name.Trim()) || (c.Code == model.Code.Trim()));
                if (oldPermission != null)
                {
                    return new OperationResult(OperationResultType.Warning, "该模块中已经存在相同名称或编码的权限，请修改后重新提交！");
                }
                var entity = new Permission
                {
                    Name = model.Name,
                    ModuleId = model.ModuleId,
                    Code = model.Code,
                    Description = model.Description,
                    Enabled = model.Enabled,
                    UpdateDate = DateTime.Now
                };
                _PermissionRepository.Insert(entity);
                return new OperationResult(OperationResultType.Success, "新增数据成功！");
            }
            catch
            {
                return new OperationResult(OperationResultType.Error, "新增数据失败，数据库插入数据时发生了错误!");
            }
        }

        public OperationResult Update(PermissionVM model)
        {
            try
            {
                var permission = Permissions.FirstOrDefault(c => c.Id == model.Id);
                if (permission == null)
                {
                    throw new Exception();
                }
                var other = Permissions.FirstOrDefault(c => c.Id != model.Id && c.ModuleId == model.ModuleId && (c.Name == model.Name.Trim() || c.Code == model.Code));
                if (other != null)
                {
                    return new OperationResult(OperationResultType.Warning, "该模块中已经存在相同名称或编码的权限，请修改后重新提交！");
                }
                permission.Name = model.Name.Trim();
                permission.ModuleId = model.ModuleId;
                permission.Code = model.Code;
                permission.Description = model.Description;
                permission.Enabled = model.Enabled;
                permission.UpdateDate = DateTime.Now;
                _PermissionRepository.Update(permission);
                return new OperationResult(OperationResultType.Success, "更新数据成功！");
            }
            catch
            {
                return new OperationResult(OperationResultType.Error, "更新数据失败!");
            }
        }
    }
}
