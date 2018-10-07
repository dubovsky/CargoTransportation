namespace kurs
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class СостояниеЗаказа
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public СостояниеЗаказа()
        {
            Заказы = new HashSet<Заказы>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int СостояниеID { get; set; }

        [Required]
        [StringLength(50)]
        public string Состояние { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Заказы> Заказы { get; set; }
    }
}
