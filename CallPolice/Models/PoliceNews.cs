using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CallPolice.Models
{
    public class PoliceNews
    {
        [Key]
        public int NewsId { get; set; }

        [Display(Name = "标题")]
        public string NewsTitle { get; set; }
        [Display(Name = "内容")]
        public string NewsContent { get; set; }
        [Display(Name = "日期")]
        public DateTime CreateTime { get; set; }
    }
}