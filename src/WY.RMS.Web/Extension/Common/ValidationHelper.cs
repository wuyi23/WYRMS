/************************************
 * 描述：尚未添加描述
 * 作者：吴毅
 * 日期：2016/2/16 11:56:32  
*************************************/

using System.Linq;
using System.Web.Mvc;

namespace WY.RMS.Web.Extension.Common
{
    public static class ValidationHelper
    {
        /// <summary>
        /// Controller中删除不需要验证的属性
        /// </summary>
        /// <param name="modelState"></param>
        /// <param name="name"></param>
        public static void RemoveValidationError(ModelStateDictionary modelState, string name)
        {
            for (var i = 0; i < modelState.Keys.Count; i++)
            {
                if (modelState.Keys.ElementAt(i) != name || modelState.Values.ElementAt(i).Errors.Count <= 0) continue;
                modelState.Values.ElementAt(i).Errors.Clear();
                break;
            }
        }
    }
}
