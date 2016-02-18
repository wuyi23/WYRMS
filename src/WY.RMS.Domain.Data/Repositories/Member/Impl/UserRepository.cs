/************************************
 * 描述：尚未添加描述
 * 作者：吴毅
 * 日期：2015/11/18 14:46:56  
*************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using EntityFramework.Extensions;
using WY.RMS.Domain.Model.Member;

namespace WY.RMS.Domain.Data.Repositories.Member.Impl
{
    public partial class UserRepository
    {
        public User GetUserByEager(Expression<Func<User, bool>> wh)
        {
            User user = (from item in Context.Users.Include("Roles").Include("UserGroups").Where(wh)
                         select item).FirstOrDefault();
            return user;
        }
    }
}
