namespace kurs
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Клиенты
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Клиенты()
        {
            Заказы = new HashSet<Заказы>();
        }

        [Key]
        public int КлиентID { get; set; }

        [Required]
        [StringLength(50)]
        public string ФИО { get; set; }

        [Required]
        [StringLength(50)]
        public string Адрес { get; set; }

        [Required]
        [StringLength(20)]
        public string Телефон { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Заказы> Заказы { get; set; }
    }
}
