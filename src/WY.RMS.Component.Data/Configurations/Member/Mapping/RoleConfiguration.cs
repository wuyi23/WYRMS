/************************************
 * 描述：尚未添加描述
 * 作者：吴毅
 * 日期：2015/11/11 14:02:29  
*************************************/

using System.ComponentModel.DataAnnotations.Schema;

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
