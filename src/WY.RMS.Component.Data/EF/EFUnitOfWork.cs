/************************************
 * 描述：尚未添加描述
 * 作者：吴毅
 * 日期：2015/9/7 17:35:17  
*************************************/

using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using WY.RMS.Component.Data.EF.Interface;
using WY.RMS.Component.Tools.helpers;

namespace WY.RMS.Component.Data.EF
{
    /// <summary>
    /// EF数据单元操作类
    /// </summary>
    public class EFUnitOfWork : IUnitOfWork
    {
        /// <summary>
        /// 获取或设置 当前使用的数据访问上下文对象
        /// </summary>
        protected DbContext Context
        {
            get
            {
                return EFContextFactory.GetCurrentDbContext();
            }
        }

        public int Commit()
        {
            try
            {
                int result = Context.SaveChanges();
                return result;
            }
            catch (DbUpdateException e)
            {
                if (e.InnerException != null && e.InnerException.InnerException is SqlException)
                {
                    SqlException sqlEx = e.InnerException.InnerException as SqlException;
                    string msg = DataHelper.GetSqlExceptionMessage(sqlEx.Number);
                    throw PublicHelper.ThrowDataAccessException("提交数据更新时发生异常：" + msg, sqlEx);
                }
                throw;
            }
        }
    }
}
