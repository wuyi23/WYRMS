/************************************
 * 描述：尚未添加描述
 * 作者：吴毅
 * 日期：2015/11/17 16:56:37  
*************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WY.RMS.ViewModel
{
    public class SideBarMenuVM
    {
        public SideBarMenuVM()
        {
            this.ChildMenuList = new List<SideBarMenuVM>();
        }

        public int Id { get; set; }
        public int? ParentId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string LinkUrl { get; set; }
        public string Area { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }

        public List<SideBarMenuVM> ChildMenuList { get; set; }
    }
}
