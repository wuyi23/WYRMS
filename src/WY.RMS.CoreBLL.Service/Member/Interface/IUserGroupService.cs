using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WY.RMS.Component.Tools;
using WY.RMS.Domain.Model.Member;
using WY.RMS.ViewModel.Member;

namespace WY.RMS.CoreBLL.Service.Member.Interface
{
    public interface IUserGroupService
    {
        IQueryable<UserGroup> UserGroups { get; }
        OperationResult Insert(UserGroupVM model);
        OperationResult Update(UserGroupVM model);
        OperationResult Delete(IEnumerable<UserGroupVM> list);
        OperationResult UpdateUserGroupRoles(int userId, string[] chkRoles);
    }
}
