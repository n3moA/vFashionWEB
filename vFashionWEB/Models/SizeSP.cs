using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace vFashionWEB.Models
{
    public class SizeSP
    {
        
        public int Masp {  get; set; }
        [Key]
        public int MaSize {  get; set; }
        public string TenSize {  get; set; }
    }
}