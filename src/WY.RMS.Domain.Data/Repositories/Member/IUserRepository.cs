using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WY.RMS.Domain.Model.Member;

namespace WY.RMS.Domain.Data.Repositories.Member
{
    partial interface IUserRepository
    {
        /// <summary>
        /// 贪婪加载User
        /// </summary>
        /// <param name="wh"></param>
        /// <returns></returns>
        User GetUserByEager(Expression<Func<User, bool>> wh);
    }
}
