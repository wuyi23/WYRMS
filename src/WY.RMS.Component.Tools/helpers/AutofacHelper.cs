/************************************
 * 描述：尚未添加描述
 * 作者：吴毅
 * 日期：2016/3/16 15:49:03  
*************************************/

using Autofac.Integration.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;

namespace WY.RMS.Component.Tools.helpers
{
    public class AutofacHelper : IAutofacHelper
    {
        public T GetByName<T>(string name)
        {
            return AutofacDependencyResolver.Current.RequestLifetimeScope.ResolveNamed<T>(name);
        }
    }


}
