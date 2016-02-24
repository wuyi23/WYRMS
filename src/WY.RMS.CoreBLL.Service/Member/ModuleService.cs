/************************************
 * 描述：尚未添加描述
 * 作者：吴毅
 * 日期：2015/11/20 9:57:40  
*************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using WY.RMS.Component.Data;
using WY.RMS.Component.Data.EF.Interface;
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

        public IQueryable<Module> Modules
        {
            get { return _moduleRepository.Entities; }
        }

        public IQueryable<Module> GetEntitiesByEager(IEnumerable<string> inclueList)
        {
            return _moduleRepository.GetEntitiesByEager(inclueList);
        }

        public IList<ModuleVM> GetListModuleVM(Expression<Func<Module, bool>> wh,int limit, int offset,  out int total)
        {
            return _moduleRepository.GetListModuleVM(wh, limit, offset,out total);
        }

    }
}