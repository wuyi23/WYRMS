/************************************
 * 描述：尚未添加描述
 * 作者：吴毅
 * 日期：2016/3/2 16:48:54  
*************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using WY.RMS.Domain.Model.Member;
using WY.RMS.ViewModel.Member;

namespace WY.RMS.Domain.Data.Repositories.Member.Impl
{
    public partial class PermissionRepository
    {
        public IList<PermissionVM> GetListPermissionVM(Expression<Func<Permission, bool>> wh, int limit, int offset, out int total)
        {
            var q = from p in Context.Permissions.Where(wh)
                    join m in Context.Modules on p.module.Id equals m.Id into joinModule
                    from item in joinModule
                    select new PermissionVM
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Code = p.Code,
                        Description = p.Description,
                        Enabled = p.Enabled,
                        UpdateDate = p.UpdateDate,
                        ModuleId = item.Id,
                        ModuleName = item.Name
                    };
            total = q.Count();
            if (offset >= 0)
            {
                return q.OrderBy(c => c.ModuleId).ThenBy(c => c.Code).Skip(offset).Take(limit).ToList();
            }
            return q.ToList();
        }
    }
}
