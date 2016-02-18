/************************************
 * 描述：尚未添加描述
 * 作者：吴毅
 * 日期：2015/11/20 9:57:40  
*************************************/

using System.Collections.Generic;
using System.Linq;
using WY.RMS.Component.Data;
using WY.RMS.Component.Data.EF.Interface;
using WY.RMS.Domain.Data.Repositories.Member;
using WY.RMS.Domain.Model.Member;

namespace WY.RMS.CoreBLL.Service
{
    public class ModuleService : CoreServiceBase, IModuleService
    {
        private readonly IModuleRepository _ModuleRepository;

        public ModuleService(IModuleRepository moduleRepository, IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            _ModuleRepository = moduleRepository;
        }

        public IQueryable<Module> Modules
        {
            get { return _ModuleRepository.Entities; }
        }

        public IQueryable<Module> GetEntitiesByEager(IEnumerable<string> inclueList)
        {
            return _ModuleRepository.GetEntitiesByEager(inclueList);
        }
    }
}