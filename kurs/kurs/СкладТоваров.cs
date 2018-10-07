namespace kurs
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class СкладТоваров
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public СкладТоваров()
        {
            ЗаказаноТоваров = new HashSet<ЗаказаноТоваров>();
        }

        [Key]
        public int КодТовара { get; set; }

        public int Остаток { get; set; }

        [Column(TypeName = "money")]
        public decimal Цена { get; set; }

        [Required]
        [StringLength(50)]
        public string Наименование { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ЗаказаноТоваров> ЗаказаноТоваров { get; set; }
    }
}
