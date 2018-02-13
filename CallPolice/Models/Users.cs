using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace CallPolice.Models
{
    public class Users
    {
        [Key]
        public int UserId { get; set; }
        [Display(Name = "姓名")]
        public string UserName { get; set; }

        
        [Required]
        [Display(Name = "手机号码")]
        public string UserCellPhone { get; set; }
        [Required]
        [Display(Name = "密码")]
        public string UserPwd { get; set; }

        [Display(Name ="家庭住址")]
        public string UserAddress { get; set; }
        [Display(Name = "应急电话")]
        public string UserRelative { get; set; }
    }
}