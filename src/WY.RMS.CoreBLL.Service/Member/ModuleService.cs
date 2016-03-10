/************************************
 * 描述：尚未添加描述
 * 作者：吴毅
 * 日期：2015/11/20 9:57:40  
*************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Transactions;
using WY.RMS.Component.Data;
using WY.RMS.Component.Data.EF.Interface;
using WY.RMS.Component.Tools;
using WY.RMS.Domain.Data.Repositories.Member;
using WY.RMS.Domain.Model.Member;
using WY.RMS.ViewModel.Member;

namespace WY.RMS.CoreBLL.Service
{
    public class ModuleService : CoreServiceBase, IModuleService
    {
        private readonly IModuleRepository _moduleRepository;

        public ModuleService(IModuleRepository moduleRepository, IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            _moduleRepository = moduleRepository;
        }

        #region 查询
        public IQueryable<Module> Modules
        {
            get { return _moduleRepository.Entities; }
        }
        /// <summary>
        /// 贪婪加载实体
        /// </summary>
        /// <param name="inclueList"></param>
        /// <returns></returns>
        public IQueryable<Module> GetEntitiesByEager(IEnumerable<string> inclueList)
        {
            return _moduleRepository.GetEntitiesByEager(inclueList);
        }
        /// <summary>
        /// 获取模块分页列表
        /// </summary>
        /// <param name="wh">查询where表达式</param>
        /// <param name="limit"></param>
        /// <param name="offset"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public IList<ModuleVM> GetListModuleVM(Expression<Func<Module, bool>> wh, int limit, int offset, out int total)
        {
            return _moduleRepository.GetListModuleVM(wh, limit, offset, out total);
        }
        #endregion


        public OperationResult Insert(ModuleVM model)
        {
            try
            {
                Module oldModule = Modules.FirstOrDefault(c => c.Name == model.Name.Trim());
                if (oldModule != null)
                {
                    return new OperationResult(OperationResultType.Warning, "数据库中已经存在相同名称的模块，请修改后重新提交！");
                }
                var entity = new Module
                {
                    Name = model.Name.Trim(),
                    ParentId = model.ParentId,
                    LinkUrl = model.LinkUrl,
                    IsMenu = model.IsMenu,
                    Code = model.Code,
                    Description = model.Description,
                    Enabled = model.Enabled,
                    UpdateDate = DateTime.Now
                };
                _moduleRepository.Insert(entity);
                return new OperationResult(OperationResultType.Success, "新增数据成功！");
            }
            catch
            {
                return new OperationResult(OperationResultType.Error, "新增数据失败，数据库插入数据时发生了错误!");
            }
        }

        public OperationResult Update(ModuleVM model)
        {
            try
            {
                var module = Modules.FirstOrDefault(c => c.Id == model.Id);
                if (module == null)
                {
                    throw new Exception();
                }
                var other = Modules.FirstOrDefault(c => c.Id != model.Id && c.Name == model.Name.Trim());
                if (other != null)
                {
                    return new OperationResult(OperationResultType.Warning, "数据库中已经存在相同名称的模块，请修改后重新提交！");
                }
                module.Name = model.Name.Trim();
                module.ParentId = model.ParentId;
                module.LinkUrl = model.LinkUrl;
                module.IsMenu = model.IsMenu;
                module.Code = model.Code;
                module.Description = model.Description;
                module.Enabled = model.Enabled;
                module.UpdateDate = DateTime.Now;
                _moduleRepository.Update(module);
                return new OperationResult(OperationResultType.Success, "更新数据成功！");
            }
            catch
            {
                return new OperationResult(OperationResultType.Error, "更新数据失败!");
            }
        }


        public OperationResult Delete(IEnumerable<ModuleVM> list)
        {
            try
            {
                using (var scope = new TransactionScope())
                {

                    foreach (var item in list)
                    {
                        _moduleRepository.Delete(item.Id, false);
                    }
                    UnitOfWork.Commit();

                    scope.Complete();
                    return new OperationResult(OperationResultType.Success, "删除数据成功！");

                }
            }
            catch
            {
                return new OperationResult(OperationResultType.Error, "删除数据失败!");
            }
        }

    }
}