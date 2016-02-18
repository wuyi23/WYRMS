using System.Linq;
using WY.RMS.Domain.Model.Member;

namespace WY.RMS.CoreBLL.Service
{
    public interface IPermissionService
    {
        #region 属性
        IQueryable<Permission> Permissions { get; }
        #endregion

        #region 公共方法

        #endregion
    }
}
