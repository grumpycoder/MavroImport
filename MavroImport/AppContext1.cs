namespace MavroImport
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class AppContext1 : DbContext
    {
        public AppContext1()
            : base("name=AppContext")
        {
        }

        public virtual DbSet<Chapter> Chapters { get; set; }
        public virtual DbSet<ChapterMediaPublishedRel> ChapterMediaPublishedRels { get; set; }
        public virtual DbSet<MediaPublished> MediaPublisheds { get; set; }
        public virtual DbSet<MediaPublishedContext> MediaPublishedContexts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Chapter>()
                .Property(e => e.ChapterName)
                .IsUnicode(false);

            modelBuilder.Entity<Chapter>()
                .Property(e => e.ChapterDesc)
                .IsUnicode(false);

            modelBuilder.Entity<Chapter>()
                .Property(e => e.RemovalReason)
                .IsUnicode(false);

            modelBuilder.Entity<Chapter>()
                .HasMany(e => e.ChapterMediaPublishedRels)
                .WithRequired(e => e.Chapter)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MediaPublished>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<MediaPublished>()
                .Property(e => e.Author)
                .IsUnicode(false);

            modelBuilder.Entity<MediaPublished>()
                .Property(e => e.RenewalPermission)
                .IsUnicode(false);

            modelBuilder.Entity<MediaPublished>()
                .Property(e => e.Summary)
                .IsUnicode(false);

            modelBuilder.Entity<MediaPublished>()
                .Property(e => e.City)
                .IsUnicode(false);

            modelBuilder.Entity<MediaPublished>()
                .Property(e => e.County)
                .IsUnicode(false);

            modelBuilder.Entity<MediaPublished>()
                .Property(e => e.RemovalReason)
                .IsUnicode(false);

            modelBuilder.Entity<MediaPublished>()
                .Property(e => e.CatalogId)
                .IsUnicode(false);

            modelBuilder.Entity<MediaPublished>()
                .HasMany(e => e.ChapterMediaPublishedRels)
                .WithRequired(e => e.MediaPublished)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MediaPublishedContext>()
                .Property(e => e.FileName)
                .IsUnicode(false);

            modelBuilder.Entity<MediaPublishedContext>()
                .Property(e => e.DocumentExtension)
                .IsUnicode(false);
        }
    }
}
