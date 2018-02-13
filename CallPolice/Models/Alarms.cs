using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace CallPolice.Models
{
    public class Alarms
    {
        [Key]
        public int AlarmId { get; set; }
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public Users User { get; set; }
        [DisplayName("经度")]
        public string Longitide { get; set; }
        [DisplayName("纬度")]
        public string Latitude { get; set; }
        [DisplayName("文件类型")]
        public int FileType { get; set; }
        [DisplayName("文件地址")]
        public string FileName { get; set; }

        public string AlarmContent { get; set; }
    }
}