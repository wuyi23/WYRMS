/************************************
 * 描述：尚未添加描述
 * 作者：吴毅
 * 日期：2015/11/12 15:17:02  
*************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WY.RMS.Component.Tools;

namespace WY.RMS.Domain.Model.Member
{
    public class Operation : EntityBase<int>
    {
        public Operation()
        {
        }

        [Description("名称")]
        public string Name { get; set; }

        [Description("操作编号")]
        public string Code { get; set; }

        [Description("描述")]
        public string Description { get; set; }

        public bool Enabled { get; set; }

        public virtual Module module { get; set; }
    }
}
