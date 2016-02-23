/************************************
 * 描述：尚未添加描述
 * 作者：吴毅
 * 日期：2015/11/11 13:59:27  
*************************************/

using System.ComponentModel.DataAnnotations.Schema;

namespace WY.RMS.Component.Data.Configurations.Member
{
    internal partial class UserConfiguration
    {
        partial void UserConfigurationAppend()
        {
            this.HasKey(c => c.Id);
            this.Property(c => c.UserName).HasColumnName("UserName").HasMaxLength(20);
            this.Property(c => c.Password).HasMaxLength(32);
            this.Property(c => c.TrueName).HasMaxLength(20);
            this.Property(c => c.Email).HasMaxLength(50);
            this.Property(c => c.Address).HasMaxLength(300);

            //this.ToTable("User");
            this.Property(t => t.Id).HasColumnName("Id").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
    }

}
