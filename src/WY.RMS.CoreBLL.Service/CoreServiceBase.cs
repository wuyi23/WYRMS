/************************************
 * 描述：尚未添加描述
 * 作者：吴毅
 * 日期：2015/9/8 13:59:57  
*************************************/

using WY.RMS.Component.Data;
using WY.RMS.Component.Data.EF.Interface;

namespace WY.RMS.CoreBLL.Service
{
    /// <summary>
    /// 核心业务实现基类
    /// </summary>
    public abstract class CoreServiceBase
    {
        protected IUnitOfWork UnitOfWork;

        protected CoreServiceBase(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }
    }
}
