using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.IO;

namespace website.Models
{
    public partial class websitedbContext : DbContext
    {
        public websitedbContext()
        {
        }

        public websitedbContext(DbContextOptions<websitedbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Messages> Messages { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                String conStr = File.ReadAllLines("connection.string")[0];
                optionsBuilder.UseNpgsql(conStr);
            }
        }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Messages>(entity =>
            {
                entity.ToTable("messages");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Heure).HasColumnName("heure");

                entity.Property(e => e.Lat)
                    .HasColumnName("lat")
                    .HasColumnType("numeric(10,0)");

                entity.Property(e => e.Long)
                    .HasColumnName("long")
                    .HasColumnType("numeric(10,0)");

                entity.Property(e => e.Messagecontent)
                    .HasColumnName("messagecontent")
                    .HasMaxLength(1000);
            });

            OnModelCreatingPartial(modelBuilder);
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
