using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace vFashionWEB.Models
{
    public class Color
    {
        [Key]
        public int MaMau { get; set; }
        public string TenMau { get; set; }
    }
}