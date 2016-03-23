/************************************
 * 描述：尚未添加描述
 * 作者：吴毅
 * 日期：2016/3/2 16:31:41  
*************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WY.RMS.ViewModel.Member
{

    public class PermissionVM
    {
        public PermissionVM()
        {
            Enabled = true;
        }
        [Display(Name = "权限ID")]
        public int Id { get; set; }

        [Required(ErrorMessage = "权限名称不能为空")]
        [Display(Name = "权限名称")]
        [StringLength(20)]
        public string Name { get; set; }


        [Required(ErrorMessage = "权限编码不能为空")]
        [StringLength(50)]
        [Display(Name = "权限编码")]
        public string Code { get; set; }


        [Display(Name = "描述")]
        [StringLength(100)]
        public string Description { get; set; }
        [Display(Name = "是否激活")]
        public bool Enabled { get; set; }

        public string StrEnabled
        {
            get
            {
                return Enabled == true ? "是" : "否";
            }
        }

        [Display(Name = "更新时间")]
        public DateTime UpdateDate { get; set; }

        public string StrUpdateDate
        {
            get
            {
                return UpdateDate.ToString();
            }
        }
        [Display(Name = "所属模块Id")]
        public int ModuleId { get; set; }


        [Display(Name = "所属模块")]
        public string ModuleName { get; set; }
    }
}
