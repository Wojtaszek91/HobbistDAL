using Models.Models;
using Models.Models.EntityFrameworkJoinEntities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models.Models.Entities;

namespace DAL.DataContext
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<GroupProfile> GroupProfiles { get; set; }
        public DbSet<UserAccount> UserAccounts { get; set; }
        public DbSet<HashTag> HashTags { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<UserMessage> UserMessages { get; set; }
        public DbSet<PostMark> PostMark { get; set; }
        public DbSet<MessageBox> MessageBoxes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region UserProfileUserAccountOneToOne

            modelBuilder.Entity<UserProfile>()
                .HasOne(up => up.UserAccount)
                .WithOne()
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<UserAccount>()
                .HasOne(ua => ua.UserProfile)
                .WithOne()
                .OnDelete(DeleteBehavior.NoAction);

            #endregion UserProfileUserAccountOneToOne

            #region UserProfileHashTagManyToMany

            modelBuilder.Entity<UserProfile>()
                .HasMany<HashTag>(s => s.HashTags)
                .WithMany(x => x.UserProfiles);              

            #endregion UserProfileHashTagManyToMany

            #region GroupProfileUserProfile

            modelBuilder.Entity<GroupProfileUserProfile>()
                .HasKey(gpua => new { gpua.GroupProfileId, gpua.ProfileId });

            modelBuilder.Entity<GroupProfileUserProfile>()
                .HasOne(gpua => gpua.GroupProfile)
                .WithMany(gp => gp.MembersId)
                .HasForeignKey(gpua => gpua.GroupProfileId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<GroupProfileUserProfile>()
                .HasOne(gpua => gpua.UserProfile)
                .WithMany(ua => ua.GroupProfiles)
                .HasForeignKey(gpua => gpua.ProfileId)
                .OnDelete(DeleteBehavior.NoAction);

            #endregion GroupProfileUserAccounts

            #region GroupProfileManagersAccounts

            modelBuilder.Entity<GroupProfileManagers>()
                .HasKey(gpua => new { gpua.GroupProfileId, gpua.UserProfileId });

            modelBuilder.Entity<GroupProfileManagers>()
                .HasOne(gpua => gpua.GroupProfile)
                .WithMany(gp => gp.ManagersId)
                .HasForeignKey(gpua => gpua.GroupProfileId).OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<GroupProfileManagers>()
                .HasOne(gpua => gpua.UserProfile)
                .WithMany(ua => ua.GroupManagers)
                .HasForeignKey(gpua => gpua.UserProfileId).OnDelete(DeleteBehavior.NoAction);

            #endregion GroupProfileManagersAccounts

            #region PostMark

            modelBuilder.Entity<PostMark>().HasKey(k => new { k.PostId, k.UserProfileId });

            modelBuilder.Entity<PostMark>()
                .HasOne(k => k.UserProfile)
                .WithMany(k => k.PostMarks)
                .HasForeignKey(k => k.UserProfileId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<PostMark>()
                .HasOne(k => k.Post)
                .WithMany(k => k.PostMarks)
                .HasForeignKey(k => k.PostId)
                .OnDelete(DeleteBehavior.NoAction);

            #endregion PostMark
        }
    }

}
