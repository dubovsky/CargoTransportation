namespace kurs
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Заказы
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Заказы()
        {
            ЗаказаноТоваров = new HashSet<ЗаказаноТоваров>();
        }

        [Key]
        public int ЗаказID { get; set; }

        [Column(TypeName = "date")]
        public DateTime СрокПоставки { get; set; }

        [Column(TypeName = "date")]
        public DateTime ДатаЗаказа { get; set; }

        public int КлиентID { get; set; }

        [Required]
        [StringLength(50)]
        public string МестоНазначения { get; set; }

        public int СостояниеID { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ДатаДоставки { get; set; }

        public int ТрСредствоID { get; set; }

        public int ВодительID { get; set; }

        public virtual Водители Водители { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ЗаказаноТоваров> ЗаказаноТоваров { get; set; }

        public virtual Клиенты Клиенты { get; set; }

        public virtual СостояниеЗаказа СостояниеЗаказа { get; set; }

        public virtual ТранспортноеСредство ТранспортноеСредство { get; set; }
    }
}
