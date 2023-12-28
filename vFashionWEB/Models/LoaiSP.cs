using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace vFashionWEB.Models
{
    public class LoaiSP
    {
        [Key]
        public int MaLoai { get; set; }
        public string TenLoai { get; set; }
    }
}