using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WY.RMS.Component.Tools;
using WY.RMS.Domain.Model.Member;
using WY.RMS.ViewModel;
using WY.RMS.ViewModel.Member;

namespace WY.RMS.CoreBLL.Service
{
    public interface IRoleService
    {
        IQueryable<Role> Roles { get; }
        OperationResult Insert(RoleVM model);
        OperationResult Update(RoleVM model);
        OperationResult Delete(IEnumerable<RoleVM> list);
        IList<ZTreeVM> GetListZTreeVM(int id);
        OperationResult UpdateAuthorize(int roleId, int[] ids);
    }
}
