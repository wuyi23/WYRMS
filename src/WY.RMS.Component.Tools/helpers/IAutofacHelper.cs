using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WY.RMS.Component.Tools.helpers
{
    public interface IAutofacHelper
    {
        /// <summary>
        /// 一个接口多个实现并定义多个Name的情况下获取的实例，配置需定义名称如builder.RegisterType<RoleService>().Named<IRoleService>("roleSc");
        /// </summary>
        /// <typeparam name="T">实例类型</typeparam>
        /// <param name="name">配置时定义名称</param>
        /// <returns></returns>
        T GetByName<T>(string name);
    }
}
