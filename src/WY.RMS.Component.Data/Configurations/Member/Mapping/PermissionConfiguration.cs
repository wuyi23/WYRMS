/************************************
 * 描述：尚未添加描述
 * 作者：吴毅
 * 日期：2015/11/13 11:37:42  
*************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WY.RMS.Component.Data.Configurations.Member
{
     partial class PermissionConfiguration
    {
        partial void PermissionConfigurationAppend()
        {
            this.Property(t => t.Id).HasColumnName("Id").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.HasMany(r => r.Roles).WithMany(u => u.Permissions);
        }
    }
}
