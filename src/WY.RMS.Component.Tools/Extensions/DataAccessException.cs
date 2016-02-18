/************************************
 * 描述：尚未添加描述
 * 作者：吴毅
 * 日期：2015/9/6 16:58:36  
*************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WY.RMS.Component.Tools
{
    /// <summary>
    ///     数据访问层异常类，用于封装数据访问层引发的异常，以供 业务逻辑层 抓取
    /// </summary>
    [Serializable]
    public class DataAccessException : Exception
    {
        /// <summary>
        ///     实体化一个 GMF.Component.Tools.DalException 类的新实例
        /// </summary>
        public DataAccessException() { }

        /// <summary>
        ///     使用异常消息实例化一个 GMF.Component.Tools.DalException 类的新实例
        /// </summary>
        /// <param name="message">异常消息</param>
        public DataAccessException(string message)
            : base(message) { }

        /// <summary>
        ///     使用异常消息与一个内部异常实例化一个 GMF.Component.Tools.DalException 类的新实例
        /// </summary>
        /// <param name="message">异常消息</param>
        /// <param name="inner">用于封装在DalException内部的异常实例</param>
        public DataAccessException(string message, Exception inner)
            : base(message, inner) { }

        /// <summary>
        ///     使用可序列化数据实例化一个 GMF.Component.Tools.DalException 类的新实例
        /// </summary>
        /// <param name="info">保存序列化对象数据的对象。</param>
        /// <param name="context">有关源或目标的上下文信息。</param>
        protected DataAccessException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }
}
