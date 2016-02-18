/************************************
 * 描述：尚未添加描述
 * 作者：吴毅
 * 日期：2015/11/13 11:41:58  
*************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WY.RMS.Component.Data.Configurations.Member
{
    internal partial class ModuleConfiguration
    {
        partial void ModuleConfigurationAppend()
        {
            this.Property(t => t.Id).HasColumnName("Id").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.HasOptional(c => c.ParentModule).WithMany(c => c.ChildModules).HasForeignKey(d => d.ParentId);
        }
    }
}
