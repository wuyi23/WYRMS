/************************************
 * 描述：尚未添加描述
 * 作者：吴毅
 * 日期：2015/11/20 15:16:38  
*************************************/

using System.Linq;
using WY.RMS.Component.Data;
using WY.RMS.Component.Data.EF.Interface;
using WY.RMS.Domain.Data.Repositories.Member;
using WY.RMS.Domain.Model.Member;

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
    }
}
