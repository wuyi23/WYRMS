/************************************
 * 描述：尚未添加描述
 * 作者：吴毅
 * 日期：2016/2/24 11:27:03  
*************************************/

using System;
using System.ComponentModel.DataAnnotations;

namespace WY.RMS.ViewModel.Member
{
    public class ModuleVM
    {
        public ModuleVM()
        {
            Enabled = true;
            IsMenu = true;
        }
        [Display(Name = "模块ID")]
        public int Id { get; set; }

        [Required(ErrorMessage = "模块名称不能为空")]
        [Display(Name = "模块名称")]
        [StringLength(20)]
        public string Name { get; set; }


        [Display(Name = "父模块ID")]
        public int? ParentId { get; set; }


        [Display(Name = "父模块名称")]
        [StringLength(20)]
        public string ParentName { get; set; }


        [Display(Name = "链接地址")]
        [StringLength(50)]
        public string LinkUrl { get; set; }

        [Display(Name = "是否是菜单")]
        public bool IsMenu { get; set; }

        [Required(ErrorMessage = "模块编号不能为空")]
        [Display(Name = "模块编号")]
        [StringLength(20)]
        public string Code { get; set; }

        [Display(Name = "描述")]
        [StringLength(100)]
        public string Description { get; set; }
        [Display(Name = "是否激活")]
        public bool Enabled { get; set; }

        public string StrIsMenu
        {
            get
            {
                return IsMenu == true ? "是" : "否";
            }
        }

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
    }
}
