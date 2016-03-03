/************************************
 * 描述：尚未添加描述
 * 作者：吴毅
 * 日期：2015/11/13 11:37:42  
*************************************/

using System.ComponentModel.DataAnnotations.Schema;

namespace WY.RMS.Component.Data.Configurations.Member
{
     partial class PermissionConfiguration
    {
        partial void PermissionConfigurationAppend()
        {
            this.Property(t => t.Id).HasColumnName("Id").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.HasMany(r => r.Roles).WithMany(u => u.Permissions);
            this.HasRequired(c => c.module).WithMany(c => c.Permissions).HasForeignKey(d => d.ModuleId); ;
        }
    }
}
