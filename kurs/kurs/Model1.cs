namespace kurs
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
        {
        }

        public virtual DbSet<Водители> Водители { get; set; }
        public virtual DbSet<ЗаказаноТоваров> ЗаказаноТоваров { get; set; }
        public virtual DbSet<Заказы> Заказы { get; set; }
        public virtual DbSet<Клиенты> Клиенты { get; set; }
        public virtual DbSet<СкладТоваров> СкладТоваров { get; set; }
        public virtual DbSet<СостояниеЗаказа> СостояниеЗаказа { get; set; }
        public virtual DbSet<ТранспортноеСредство> ТранспортноеСредство { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Водители>()
                .Property(e => e.ФИО)
                .IsUnicode(false);

            modelBuilder.Entity<Водители>()
                .Property(e => e.МобТелефон)
                .IsUnicode(false);

            modelBuilder.Entity<Водители>()
                .HasMany(e => e.Заказы)
                .WithRequired(e => e.Водители)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ЗаказаноТоваров>()
                .Property(e => e.РасценкаТоннЗаКм)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Заказы>()
                .Property(e => e.МестоНазначения)
                .IsUnicode(false);

            modelBuilder.Entity<Заказы>()
                .HasMany(e => e.ЗаказаноТоваров)
                .WithRequired(e => e.Заказы)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Клиенты>()
                .Property(e => e.ФИО)
                .IsUnicode(false);

            modelBuilder.Entity<Клиенты>()
                .Property(e => e.Адрес)
                .IsUnicode(false);

            modelBuilder.Entity<Клиенты>()
                .Property(e => e.Телефон)
                .IsUnicode(false);

            modelBuilder.Entity<Клиенты>()
                .HasMany(e => e.Заказы)
                .WithRequired(e => e.Клиенты)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<СкладТоваров>()
                .Property(e => e.Цена)
                .HasPrecision(19, 4);

            modelBuilder.Entity<СкладТоваров>()
                .Property(e => e.Наименование)
                .IsUnicode(false);

            modelBuilder.Entity<СкладТоваров>()
                .HasMany(e => e.ЗаказаноТоваров)
                .WithRequired(e => e.СкладТоваров)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<СостояниеЗаказа>()
                .Property(e => e.Состояние)
                .IsUnicode(false);

            modelBuilder.Entity<СостояниеЗаказа>()
                .HasMany(e => e.Заказы)
                .WithRequired(e => e.СостояниеЗаказа)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ТранспортноеСредство>()
                .Property(e => e.Марка)
                .IsUnicode(false);

            modelBuilder.Entity<ТранспортноеСредство>()
                .HasMany(e => e.Заказы)
                .WithRequired(e => e.ТранспортноеСредство)
                .WillCascadeOnDelete(false);
        }
    }
}
