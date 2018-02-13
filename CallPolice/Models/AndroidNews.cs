using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CallPolice.Models
{
    public class AndroidNews
    {
        public int NewsId { get; set; }

       
        public string NewsTitle { get; set; }
        
        public string NewsContent { get; set; }
       
        public string CreateTime { get; set; }
    }
}