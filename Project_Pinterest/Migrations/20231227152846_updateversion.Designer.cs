﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Project_Pinterest.DataContexts;

#nullable disable

namespace Project_Pinterest.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20231227152846_updateversion")]
    partial class updateversion
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Project_Pinterest.Entities.Collection", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("collections");
                });

            modelBuilder.Entity("Project_Pinterest.Entities.ConfirmEmail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ConfirmCode")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("Confirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime>("ExpiredTime")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("confirmEmails");
                });

            modelBuilder.Entity("Project_Pinterest.Entities.Post", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreateAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool?>("IsActive")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool?>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<int?>("NumberOfComments")
                        .HasColumnType("int");

                    b.Property<int?>("NumberOfLikes")
                        .HasColumnType("int");

                    b.Property<int>("PostStatusId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("RemoveAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("UpdateAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PostStatusId");

                    b.HasIndex("UserId");

                    b.ToTable("posts");
                });

            modelBuilder.Entity("Project_Pinterest.Entities.PostCollection", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("CollectionId")
                        .HasColumnType("int");

                    b.Property<int>("PostId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CollectionId");

                    b.HasIndex("PostId");

                    b.ToTable("postCollections");
                });

            modelBuilder.Entity("Project_Pinterest.Entities.PostStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("postsStatus");
                });

            modelBuilder.Entity("Project_Pinterest.Entities.RefreshToken", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("ExpiredTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("refreshTokens");
                });

            modelBuilder.Entity("Project_Pinterest.Entities.Relationship", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("FollowerId")
                        .HasColumnType("int");

                    b.Property<int?>("FollowingId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("FollowerId");

                    b.HasIndex("FollowingId");

                    b.ToTable("relationships");
                });

            modelBuilder.Entity("Project_Pinterest.Entities.Report", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreateAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("PostId")
                        .HasColumnType("int");

                    b.Property<string>("Reason")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("UserReportId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PostId");

                    b.HasIndex("UserReportId");

                    b.ToTable("reports");
                });

            modelBuilder.Entity("Project_Pinterest.Entities.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("roles");
                });

            modelBuilder.Entity("Project_Pinterest.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("AvatarUrl")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool?>("IsActive")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool?>("IsLocked")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int?>("RoleId")
                        .HasColumnType("int");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("UserStatusId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserStatusId");

                    b.ToTable("users");
                });

            modelBuilder.Entity("Project_Pinterest.Entities.UserCommentPost", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("CreateAt")
                        .HasColumnType("datetime(6)");

                    b.Property<bool?>("IsActive")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool?>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<int?>("NumberOfLikes")
                        .HasColumnType("int");

                    b.Property<int>("PostId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("RemoveAt")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("UpdateAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PostId");

                    b.HasIndex("UserId");

                    b.ToTable("userCommentPosts");
                });

            modelBuilder.Entity("Project_Pinterest.Entities.UserLikeCommentOfPost", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("LikeTime")
                        .HasColumnType("datetime(6)");

                    b.Property<bool?>("Unlike")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("UserCommentPostId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserCommentPostId");

                    b.HasIndex("UserId");

                    b.ToTable("userLikeCommentOfPosts");
                });

            modelBuilder.Entity("Project_Pinterest.Entities.UserLikePost", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("LikeTime")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("PostId")
                        .HasColumnType("int");

                    b.Property<bool?>("Unlike")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PostId");

                    b.HasIndex("UserId");

                    b.ToTable("userLikePosts");
                });

            modelBuilder.Entity("Project_Pinterest.Entities.UserStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("userStatuses");
                });

            modelBuilder.Entity("Project_Pinterest.Entities.Collection", b =>
                {
                    b.HasOne("Project_Pinterest.Entities.User", "User")
                        .WithMany("Collections")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Project_Pinterest.Entities.ConfirmEmail", b =>
                {
                    b.HasOne("Project_Pinterest.Entities.User", "User")
                        .WithMany("ConfirmEmails")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Project_Pinterest.Entities.Post", b =>
                {
                    b.HasOne("Project_Pinterest.Entities.PostStatus", "PostStatus")
                        .WithMany("Posts")
                        .HasForeignKey("PostStatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Project_Pinterest.Entities.User", "User")
                        .WithMany("Posts")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PostStatus");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Project_Pinterest.Entities.PostCollection", b =>
                {
                    b.HasOne("Project_Pinterest.Entities.Collection", "Collection")
                        .WithMany("PostCollections")
                        .HasForeignKey("CollectionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Project_Pinterest.Entities.Post", "Post")
                        .WithMany()
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Collection");

                    b.Navigation("Post");
                });

            modelBuilder.Entity("Project_Pinterest.Entities.RefreshToken", b =>
                {
                    b.HasOne("Project_Pinterest.Entities.User", "User")
                        .WithMany("RefreshTokens")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Project_Pinterest.Entities.Relationship", b =>
                {
                    b.HasOne("Project_Pinterest.Entities.User", "Follower")
                        .WithMany()
                        .HasForeignKey("FollowerId");

                    b.HasOne("Project_Pinterest.Entities.User", "Following")
                        .WithMany()
                        .HasForeignKey("FollowingId");

                    b.Navigation("Follower");

                    b.Navigation("Following");
                });

            modelBuilder.Entity("Project_Pinterest.Entities.Report", b =>
                {
                    b.HasOne("Project_Pinterest.Entities.Post", "Post")
                        .WithMany()
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Project_Pinterest.Entities.User", "UserReport")
                        .WithMany()
                        .HasForeignKey("UserReportId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Post");

                    b.Navigation("UserReport");
                });

            modelBuilder.Entity("Project_Pinterest.Entities.User", b =>
                {
                    b.HasOne("Project_Pinterest.Entities.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId");

                    b.HasOne("Project_Pinterest.Entities.UserStatus", "UserStatus")
                        .WithMany("Users")
                        .HasForeignKey("UserStatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("UserStatus");
                });

            modelBuilder.Entity("Project_Pinterest.Entities.UserCommentPost", b =>
                {
                    b.HasOne("Project_Pinterest.Entities.Post", "Post")
                        .WithMany("UserCommentPosts")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Project_Pinterest.Entities.User", "User")
                        .WithMany("UserCommentPosts")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Post");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Project_Pinterest.Entities.UserLikeCommentOfPost", b =>
                {
                    b.HasOne("Project_Pinterest.Entities.UserCommentPost", "UserCommentPost")
                        .WithMany("UserLikeCommentOfPosts")
                        .HasForeignKey("UserCommentPostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Project_Pinterest.Entities.User", "User")
                        .WithMany("UserLikeCommentOfPosts")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");

                    b.Navigation("UserCommentPost");
                });

            modelBuilder.Entity("Project_Pinterest.Entities.UserLikePost", b =>
                {
                    b.HasOne("Project_Pinterest.Entities.Post", "Post")
                        .WithMany("UserLikePosts")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Project_Pinterest.Entities.User", "User")
                        .WithMany("UserLikePosts")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Post");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Project_Pinterest.Entities.Collection", b =>
                {
                    b.Navigation("PostCollections");
                });

            modelBuilder.Entity("Project_Pinterest.Entities.Post", b =>
                {
                    b.Navigation("UserCommentPosts");

                    b.Navigation("UserLikePosts");
                });

            modelBuilder.Entity("Project_Pinterest.Entities.PostStatus", b =>
                {
                    b.Navigation("Posts");
                });

            modelBuilder.Entity("Project_Pinterest.Entities.Role", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("Project_Pinterest.Entities.User", b =>
                {
                    b.Navigation("Collections");

                    b.Navigation("ConfirmEmails");

                    b.Navigation("Posts");

                    b.Navigation("RefreshTokens");

                    b.Navigation("UserCommentPosts");

                    b.Navigation("UserLikeCommentOfPosts");

                    b.Navigation("UserLikePosts");
                });

            modelBuilder.Entity("Project_Pinterest.Entities.UserCommentPost", b =>
                {
                    b.Navigation("UserLikeCommentOfPosts");
                });

            modelBuilder.Entity("Project_Pinterest.Entities.UserStatus", b =>
                {
                    b.Navigation("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
