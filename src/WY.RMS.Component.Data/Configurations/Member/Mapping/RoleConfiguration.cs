/************************************
 * 描述：尚未添加描述
 * 作者：吴毅
 * 日期：2015/11/11 14:02:29  
*************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WY.RMS.Domain.Model.Member;

namespace WY.RMS.Component.Data.Configurations.Member
{
    internal partial class RoleConfiguration
    {
        partial void RoleConfigurationAppend()
        {
            this.HasMany(r => r.Users).WithMany(u => u.Roles);
            this.Property(t => t.Id).HasColumnName("Id").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
    }
}
