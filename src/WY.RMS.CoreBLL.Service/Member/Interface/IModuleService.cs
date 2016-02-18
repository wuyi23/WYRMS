using System.Collections.Generic;
using System.Linq;
using WY.RMS.Domain.Model.Member;

namespace WY.RMS.CoreBLL.Service
{
    public interface IModuleService
    {
        #region 属性
        IQueryable<Module> Modules { get; }
        #endregion

        #region 公共方法
        /// <summary>
        /// 贪婪加载Module实体数据集
        /// </summary>
        /// <param name="inclueList"></param>
        /// <returns></returns>
        IQueryable<Module> GetEntitiesByEager(IEnumerable<string> inclueList);
        #endregion
    }
}
