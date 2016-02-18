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
using WY.RMS.Domain.Model.Enum;

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
        [Description("权限类型")]
        public int PermissionType { get; set; }

        [Required]
        [Description("相关权限类型表主键")]
        public int TypeKey { get; set; }



        /// <summary>
        /// 角色集合
        /// </summary>
        public virtual ICollection<Role> Roles { get; set; }

    }

}
