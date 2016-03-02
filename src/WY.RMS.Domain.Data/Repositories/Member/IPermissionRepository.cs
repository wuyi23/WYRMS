using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using WY.RMS.Domain.Model.Member;
using WY.RMS.ViewModel.Member;

namespace WY.RMS.Domain.Data.Repositories.Member
{
    public partial interface IPermissionRepository
    {
        IList<PermissionVM> GetListPermissionVM(Expression<Func<Permission, bool>> wh, int limit, int offset, out int total);
    }
}
