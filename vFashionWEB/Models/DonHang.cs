namespace vFashionWEB.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DonHang")]
    public partial class DonHang
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DonHang()
        {
            CTDonHangs = new HashSet<CTDonHang>();
        }

        [Key]
        public int Madon { get; set; }

        public DateTime? Ngaydat { get; set; }
        public DateTime? Ngaygiao { get; set; }

        public int? Tinhtrang { get; set; }
        public int? Tinhtrangthanhtoan { get; set; }

        public int? MaNguoidung { get; set; }

        [StringLength(50)]
        public string ShipName { get; set; }

        [StringLength(10)]
        public string ShipTel { get; set; }

        [StringLength(50)]
        public string ShipEmail { get; set; }

        [StringLength(100)]
        public string ShipAddress { get; set; }

        [StringLength(100)]
        public string Status { get; set; }

        public decimal? Tongthanhtien { get; set; }

        public bool? ThanhCong { get; set; }



        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CTDonHang> CTDonHangs { get; set; }

        public virtual NguoiDung NguoiDung { get; set; }
        public virtual ThanhToan ThanhToan { get; set; }

        public virtual TinhTrang TinhTrang1 { get; set; }
    }
}
