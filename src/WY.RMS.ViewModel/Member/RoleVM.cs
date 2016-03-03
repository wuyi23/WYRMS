/************************************
 * 描述：尚未添加描述
 * 作者：吴毅
 * 日期：2015/11/26 17:25:36  
*************************************/

using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace WY.RMS.ViewModel.Member
{
    public class RoleVM
    {
        public RoleVM()
        {
            Enabled = true;
            OrderSort = 9999;
        }
        [Display(Name = "角色ID")]
        public int Id { get; set; }

        [Required(ErrorMessage = "角色名称不能为空")]
        [Display(Name = "角色名称")]
        [StringLength(20)]
        public string RoleName { get; set; }

        [Display(Name = "描述")]
        [StringLength(100)]
        public string Description { get; set; }
        [Display(Name = "是否激活")]
        public bool Enabled { get; set; }

        public string StrEnabled
        {
            get {
                return Enabled == true ? "是" : "否";
            }
        }
        [Display(Name = "排序号")]
        [RegularExpression(@"\d+", ErrorMessage = "排序必须是整数")]
        [Range(1, 9999, ErrorMessage = "排序数值范围必须为{1}到{2}")]
        public int OrderSort { get; set; }
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
