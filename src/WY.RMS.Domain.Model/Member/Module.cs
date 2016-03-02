/************************************
 * 描述：尚未添加描述
 * 作者：吴毅
 * 日期：2015/9/16 15:58:46  
*************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WY.RMS.Component.Tools;

namespace WY.RMS.Domain.Model.Member
{
    /// <summary>
    /// 模块---实体
    /// </summary>
    [Description("模块")]
    public class Module : EntityBase<int>
    {
        public Module()
        {
            this.Permissions = new List<Permission>();
            this.ChildModules = new List<Module>();
        }

        [Description("父模块Id")]
        public int? ParentId { get; set; }
        [Required]
        [Description("名称")]
        [StringLength(20)]
        public string Name { get; set; }
        [Required]
        [Description("链接地址")]
        [StringLength(50)]
        public string LinkUrl { get; set; }

        [Description("是否是菜单")]
        public bool IsMenu { get; set; }
        [Description("模块编号")]
        public int Code { get; set; }
        [Description("描述")]
        [StringLength(100)]
        public string Description { get; set; }
        [Description("是否激活")]
        public bool Enabled { get; set; }

        public virtual Module ParentModule { get; set; }

        public virtual ICollection<Module> ChildModules { get; set; }

        /// <summary>
        /// 权限集合
        /// </summary>
        public virtual ICollection<Permission> Permissions { get; set; }
    }
}
