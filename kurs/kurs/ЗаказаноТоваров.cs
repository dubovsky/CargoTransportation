namespace kurs
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ЗаказаноТоваров
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ЗаказID { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int КодТовара { get; set; }

        [Column(TypeName = "money")]
        public decimal РасценкаТоннЗаКм { get; set; }

        public int Количество { get; set; }

        public int Масса { get; set; }

        public virtual Заказы Заказы { get; set; }

        public virtual СкладТоваров СкладТоваров { get; set; }
    }
}
