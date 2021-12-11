﻿// <auto-generated />
using System;
using DAL.DataContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DAL.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.11")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Models.Models.EntityFrameworkJoinEntities.GroupProfileManagers", b =>
                {
                    b.Property<int>("GroupProfileId")
                        .HasColumnType("int");

                    b.Property<int>("UserProfileId")
                        .HasColumnType("int");

                    b.HasKey("GroupProfileId", "UserProfileId");

                    b.HasIndex("UserProfileId");

                    b.ToTable("GroupProfileManagers");
                });

            modelBuilder.Entity("Models.Models.EntityFrameworkJoinEntities.GroupProfileUserProfile", b =>
                {
                    b.Property<int>("GroupProfileId")
                        .HasColumnType("int");

                    b.Property<int>("ProfileId")
                        .HasColumnType("int");

                    b.HasKey("GroupProfileId", "ProfileId");

                    b.HasIndex("ProfileId");

                    b.ToTable("GroupProfileUserProfile");
                });

            modelBuilder.Entity("Models.Models.EntityFrameworkJoinEntities.UserProfileHashTag", b =>
                {
                    b.Property<int>("HashTagId")
                        .HasColumnType("int");

                    b.Property<int>("UserProfileId")
                        .HasColumnType("int");

                    b.HasKey("HashTagId", "UserProfileId");

                    b.HasIndex("UserProfileId");

                    b.ToTable("UserProfileHashTags");
                });

            modelBuilder.Entity("Models.Models.HashTag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("HashTagName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Popularity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("HashTags");
                });

            modelBuilder.Entity("Models.Models.Post", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AverageMark")
                        .HasColumnType("int");

                    b.Property<DateTime>("BeginDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("ChainedTagId")
                        .HasColumnType("int");

                    b.Property<int>("DayLast")
                        .HasColumnType("int");

                    b.Property<string>("Followers")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsBlocked")
                        .HasColumnType("bit");

                    b.Property<decimal>("Lat")
                        .HasColumnType("decimal(27,25)");

                    b.Property<decimal>("Lng")
                        .HasColumnType("decimal(27,25)");

                    b.Property<string>("PostMessage")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PostViews")
                        .HasColumnType("int");

                    b.Property<int?>("UserAccountId")
                        .HasColumnType("int");

                    b.Property<int>("UserProfileId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ChainedTagId");

                    b.HasIndex("UserAccountId");

                    b.HasIndex("UserProfileId");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("Models.Models.UserAccount", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UserProfileId")
                        .HasColumnType("int");

                    b.Property<bool>("isBlocked")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("UserProfileId")
                        .IsUnique()
                        .HasFilter("[UserProfileId] IS NOT NULL");

                    b.ToTable("UserAccounts");
                });

            modelBuilder.Entity("Models.Models.UserMessage", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Content")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("RecivedTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("SendTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("SenderProfileId")
                        .HasColumnType("int");

                    b.Property<int>("TargetProfileId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("UserMessages");
                });

            modelBuilder.Entity("Models.Models.UserProfile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProfilePhoto")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ProfileViews")
                        .HasColumnType("int");

                    b.Property<int>("UserAccountId")
                        .HasColumnType("int");

                    b.Property<int?>("UserProfileId")
                        .HasColumnType("int");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VideoLink")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("UserAccountId")
                        .IsUnique();

                    b.HasIndex("UserProfileId");

                    b.ToTable("UserProfiles");

                    b.HasDiscriminator<string>("Discriminator").HasValue("UserProfile");
                });

            modelBuilder.Entity("Models.Models.GroupProfile", b =>
                {
                    b.HasBaseType("Models.Models.UserProfile");

                    b.HasDiscriminator().HasValue("GroupProfile");
                });

            modelBuilder.Entity("Models.Models.EntityFrameworkJoinEntities.GroupProfileManagers", b =>
                {
                    b.HasOne("Models.Models.GroupProfile", "GroupProfile")
                        .WithMany("ManagersId")
                        .HasForeignKey("GroupProfileId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Models.Models.UserProfile", "UserProfile")
                        .WithMany("GroupManagers")
                        .HasForeignKey("UserProfileId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("GroupProfile");

                    b.Navigation("UserProfile");
                });

            modelBuilder.Entity("Models.Models.EntityFrameworkJoinEntities.GroupProfileUserProfile", b =>
                {
                    b.HasOne("Models.Models.GroupProfile", "GroupProfile")
                        .WithMany("MembersId")
                        .HasForeignKey("GroupProfileId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Models.Models.UserProfile", "UserProfile")
                        .WithMany("GroupProfiles")
                        .HasForeignKey("ProfileId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("GroupProfile");

                    b.Navigation("UserProfile");
                });

            modelBuilder.Entity("Models.Models.EntityFrameworkJoinEntities.UserProfileHashTag", b =>
                {
                    b.HasOne("Models.Models.HashTag", "HashTag")
                        .WithMany("UserAccountHashTags")
                        .HasForeignKey("HashTagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Models.Models.UserProfile", "UserProfile")
                        .WithMany("UserProfileHashTags")
                        .HasForeignKey("UserProfileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("HashTag");

                    b.Navigation("UserProfile");
                });

            modelBuilder.Entity("Models.Models.Post", b =>
                {
                    b.HasOne("Models.Models.HashTag", "ChainedTag")
                        .WithMany()
                        .HasForeignKey("ChainedTagId");

                    b.HasOne("Models.Models.UserAccount", null)
                        .WithMany("Posts")
                        .HasForeignKey("UserAccountId");

                    b.HasOne("Models.Models.UserProfile", "UserProfile")
                        .WithMany("Posts")
                        .HasForeignKey("UserProfileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ChainedTag");

                    b.Navigation("UserProfile");
                });

            modelBuilder.Entity("Models.Models.UserAccount", b =>
                {
                    b.HasOne("Models.Models.UserProfile", "UserProfile")
                        .WithOne()
                        .HasForeignKey("Models.Models.UserAccount", "UserProfileId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("UserProfile");
                });

            modelBuilder.Entity("Models.Models.UserProfile", b =>
                {
                    b.HasOne("Models.Models.UserAccount", "UserAccount")
                        .WithOne()
                        .HasForeignKey("Models.Models.UserProfile", "UserAccountId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Models.Models.UserProfile", null)
                        .WithMany("FollowersId")
                        .HasForeignKey("UserProfileId");

                    b.Navigation("UserAccount");
                });

            modelBuilder.Entity("Models.Models.HashTag", b =>
                {
                    b.Navigation("UserAccountHashTags");
                });

            modelBuilder.Entity("Models.Models.UserAccount", b =>
                {
                    b.Navigation("Posts");
                });

            modelBuilder.Entity("Models.Models.UserProfile", b =>
                {
                    b.Navigation("FollowersId");

                    b.Navigation("GroupManagers");

                    b.Navigation("GroupProfiles");

                    b.Navigation("Posts");

                    b.Navigation("UserProfileHashTags");
                });

            modelBuilder.Entity("Models.Models.GroupProfile", b =>
                {
                    b.Navigation("ManagersId");

                    b.Navigation("MembersId");
                });
#pragma warning restore 612, 618
        }
    }
}
