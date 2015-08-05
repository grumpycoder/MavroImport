namespace MavroImport
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

        public virtual DbSet<BeholderPerson> BeholderPeople { get; set; }
        public virtual DbSet<CommonPerson> CommonPeople { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BeholderPerson>()
                .Property(e => e.DistinguishableMarks)
                .IsUnicode(false);

            modelBuilder.Entity<BeholderPerson>()
                .Property(e => e.RemovalReason)
                .IsUnicode(false);

            modelBuilder.Entity<CommonPerson>()
                .Property(e => e.SSN)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<CommonPerson>()
                .Property(e => e.FName)
                .IsUnicode(false);

            modelBuilder.Entity<CommonPerson>()
                .Property(e => e.MName)
                .IsUnicode(false);

            modelBuilder.Entity<CommonPerson>()
                .Property(e => e.LName)
                .IsUnicode(false);

            modelBuilder.Entity<CommonPerson>()
                .Property(e => e.DriversLicenseNumber)
                .IsUnicode(false);

            modelBuilder.Entity<CommonPerson>()
                .Property(e => e.Height)
                .IsUnicode(false);

            modelBuilder.Entity<CommonPerson>()
                .Property(e => e.Weight)
                .IsUnicode(false);
        }
    }
}
