/************************************
* 描述：尚未添加描述
* 作者：吴毅
* 日期：2015/9/15 16:26:44  
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
    /// 权限---实体
    /// </summary>
    [Description("权限")]
    public class Permission : EntityBase<int>
    {
        public Permission()
        {
            this.Roles = new List<Role>();
        }
        [Required]
        [Description("名称")]
        [StringLength(20)]
        public string Name { get; set; }

        [Description("权限编码")]
        public string Code { get; set; }

        [Description("描述")]
        [StringLength(100)]
        public string Description { get; set; }

        public bool Enabled { get; set; }

        [Description("所属模块Id")]
        public int ModuleId { get; set; }

        public virtual Module module { get; set; }

        /// <summary>
        /// 角色集合
        /// </summary>   
        public virtual ICollection<Role> Roles { get; set; }

    }

}
