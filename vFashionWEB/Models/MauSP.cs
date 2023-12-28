using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace vFashionWEB.Models
{
    public class MauSP
    {
        
        public int Masp {  get; set; }
        [Key]
        public int MaMau {  get; set; }
        public string TenMau { get; set; }
    }
}