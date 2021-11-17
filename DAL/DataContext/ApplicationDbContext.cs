﻿using DAL.Models;
using DAL.Models.EntityFrameworkJoinEntities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        public DbSet<UserAccountHashTag> UserAccountHashTags{ get; set; }
        public DbSet<Post> Posts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Post>().Property(p => p.FollowersId)
            //    .HasConversion(
            //        v => string.Join(',', v),
            //        v => v.Split(',', StringSplitOptions.RemoveEmptyEntries));

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

            #region UserAccountHashTagManyToMany

            modelBuilder.Entity<UserAccountHashTag>()
                .HasKey(uaht => new { uaht.HashTagId, uaht.UserAccountId });

            modelBuilder.Entity<UserAccountHashTag>()
                .HasOne(uaht => uaht.HashTag)
                .WithMany(h => h.UserAccountHashTags)
                .HasForeignKey(uaht => uaht.HashTagId);

            modelBuilder.Entity<UserAccountHashTag>()
                .HasOne(uath => uath.UserAccount)
                .WithMany(ua => ua.UserAccountHashTags)
                .HasForeignKey(uath => uath.UserAccountId);

            #endregion UserAccountHashTagManyToMany

            #region GroupProfileUserAccounts

            modelBuilder.Entity<GroupProfileUserAccount>()
                .HasKey(gpua => new { gpua.GroupProfileId, gpua.UserAccountId });

            modelBuilder.Entity<GroupProfileUserAccount>()
                .HasOne(gpua => gpua.GroupProfile)
                .WithMany(gp => gp.MembersId)
                .HasForeignKey(gpua => gpua.GroupProfileId);

            modelBuilder.Entity<GroupProfileUserAccount>()
                .HasOne(gpua => gpua.UserAccount)
                .WithMany(ua => ua.GroupProfiles)
                .HasForeignKey(gpua => gpua.UserAccountId);

            #endregion GroupProfileUserAccounts

            #region GroupProfileManagersAccounts

            modelBuilder.Entity<GroupProfileManagers>()
                .HasKey(gpua => new { gpua.GroupProfileId, gpua.UserAccountManagerId });

            modelBuilder.Entity<GroupProfileManagers>()
                .HasOne(gpua => gpua.GroupProfileManager)
                .WithMany(gp => gp.ManagersId)
                .HasForeignKey(gpua => gpua.GroupProfileId);

            modelBuilder.Entity<GroupProfileManagers>()
                .HasOne(gpua => gpua.UserAccountManager)
                .WithMany(ua => ua.GroupManagers)
                .HasForeignKey(gpua => gpua.UserAccountManagerId);

            #endregion GroupProfileManagersAccounts
        }
    }

}
