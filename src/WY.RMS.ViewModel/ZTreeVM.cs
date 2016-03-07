/************************************
 * 描述：尚未添加描述
 * 作者：吴毅
 * 日期：2016/3/7 15:43:26  
*************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WY.RMS.ViewModel
{
    public class ZTreeVM
    {
        public double id { get; set; }

        public int? pId { get; set; }

        public string name { get; set; }

        public bool @checked
        {
            get;
            set;
        }
        public bool isParent { get; set; }

        public bool open { get; set; }
    }
}
