using System.Collections.Generic;
using System.Linq;
using WY.RMS.Component.Tools;
using WY.RMS.Domain.Model.Member;
using WY.RMS.ViewModel.Member;

namespace WY.RMS.CoreBLL.Service.Member.Interface
{
    public interface IUserService
    {
        IQueryable<User> Users { get; }
        OperationResult Insert(UserVM model);
        OperationResult Update(UserVM model);
        OperationResult Delete(IEnumerable<UserVM> list);
        OperationResult ResetPassword(IEnumerable<UserVM> list);
        OperationResult UpdateUserRoles(int roleId, string[] chkRoles);
        OperationResult UpdateUserGroups(int userId, string[] chkUserGroups);
    }
}
