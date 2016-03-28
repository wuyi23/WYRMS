using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using WY.RMS.Component.Tools;
using WY.RMS.Domain.Model.Member;
using WY.RMS.ViewModel.Member;

namespace WY.RMS.CoreBLL.Service.Member.Interface
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

        /// <summary>
        /// 获取模块分页列表
        /// </summary>
        /// <param name="wh"></param>
        /// <param name="limit"></param>
        /// <param name="offset"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        IList<ModuleVM> GetListModuleVM(Expression<Func<Module, bool>> wh, int limit, int offset, out int total);

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        OperationResult Insert(ModuleVM model);
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        OperationResult Update(ModuleVM model);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        OperationResult Delete(IEnumerable<ModuleVM> list);

        #endregion
    }
}
