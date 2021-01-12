using FileDriveWebAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileDriveWebAPI.Data
{
    public class FileDriveContext: DbContext
    {

        public FileDriveContext(DbContextOptions<FileDriveContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<TreeEntity> TreeEntities { get; set; }
        public DbSet<Permission> Permissions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            try
            {

                modelBuilder.Entity<TreeEntity>(entity =>
                {
                    entity.HasKey(x => x.Id);
                    entity.HasOne(d => d.Parent)
                    .WithMany(d => d.Children)
                    .HasForeignKey(d => d.ParentId)
                    .IsRequired(false)
                    .OnDelete(DeleteBehavior.Restrict);
                    entity.ToTable("Tree_Entities");
                });

                modelBuilder.Entity<User>().ToTable("Users");
                modelBuilder.Entity<Permission>().ToTable("Permissions");

            } catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            base.OnModelCreating(modelBuilder);
        }
    }
}
