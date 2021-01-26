using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ViverBackend.Entities.Models;

namespace ViverBackend.Entities
{
    public class ViverContext : DbContext
    {
        public ViverContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }

        public DbSet<Post> Posts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

 

            modelBuilder.Entity<User>()
                .HasMany(u => u.FollowedUsers)
                .WithOne(fu => fu.Follows)
                .HasForeignKey(fu => fu.FollowsId)
                .OnDelete(DeleteBehavior.Restrict);

 

            modelBuilder.Entity<User>()
                .HasMany(u => u.FollowsUsers)
                .WithOne(fu => fu.FollowedBy)
                .HasForeignKey(fu => fu.FollowedById)
                .OnDelete(DeleteBehavior.Restrict);

 

            modelBuilder.Entity<User>()
                .HasQueryFilter(u => !u.IsDeleted);
        }
    }
}
