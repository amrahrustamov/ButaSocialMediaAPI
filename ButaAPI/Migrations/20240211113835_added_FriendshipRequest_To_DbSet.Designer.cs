﻿// <auto-generated />
using System;
using System.Collections.Generic;
using ButaAPI.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ButaAPI.Migrations
{
    [DbContext(typeof(ButaDbContext))]
    [Migration("20240211113835_added_FriendshipRequest_To_DbSet")]
    partial class added_FriendshipRequest_To_DbSet
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.26")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ButaAPI.Database.Model.Blog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Content")
                        .HasColumnType("text");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Image")
                        .HasColumnType("text");

                    b.Property<bool>("IsPublic")
                        .HasColumnType("boolean");

                    b.Property<int?>("LocationId")
                        .HasColumnType("integer");

                    b.Property<int>("OwnerId")
                        .HasColumnType("integer");

                    b.Property<List<string>>("Tags")
                        .HasColumnType("text[]");

                    b.HasKey("Id");

                    b.HasIndex("LocationId");

                    b.HasIndex("OwnerId");

                    b.ToTable("Blogs");
                });

            modelBuilder.Entity("ButaAPI.Database.Model.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("BlogId")
                        .HasColumnType("integer");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("OwnerId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("BlogId");

                    b.HasIndex("OwnerId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("ButaAPI.Database.Model.FriendshipRequest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("ReceiverId")
                        .HasColumnType("integer");

                    b.Property<int>("SenderId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("SenderId");

                    b.ToTable("FriendshipsRequests");
                });

            modelBuilder.Entity("ButaAPI.Database.Model.Like", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("BlogId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("OwnerId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("BlogId");

                    b.HasIndex("OwnerId");

                    b.ToTable("Likes");
                });

            modelBuilder.Entity("ButaAPI.Database.Model.Location", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("City")
                        .HasColumnType("text");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("House")
                        .HasColumnType("text");

                    b.Property<string>("PostalCode")
                        .HasColumnType("text");

                    b.Property<string>("Region")
                        .HasColumnType("text");

                    b.Property<string>("Street")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Locations");
                });

            modelBuilder.Entity("ButaAPI.Database.Model.Message", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("ReceiverId")
                        .HasColumnType("integer");

                    b.Property<int>("SenderId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("SendingTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ReceiverId");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("ButaAPI.Database.Model.Notifications", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("OwnerId")
                        .HasColumnType("integer");

                    b.Property<bool>("Read")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("Notifications");
                });

            modelBuilder.Entity("ButaAPI.Database.Model.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("AboutUser")
                        .HasColumnType("text");

                    b.Property<List<string>>("Activities")
                        .HasColumnType("text[]");

                    b.Property<DateTime?>("Birthday")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int?>("CurrentLocationId")
                        .HasColumnType("integer");

                    b.Property<List<string>>("Education")
                        .HasColumnType("text[]");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("Gender")
                        .HasColumnType("integer");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsPrivate")
                        .HasColumnType("boolean");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<string>("ProfileImage")
                        .HasColumnType("text");

                    b.Property<int>("RegisterStatus")
                        .HasColumnType("integer");

                    b.Property<int?>("Relationship")
                        .HasColumnType("integer");

                    b.Property<int?>("UserId")
                        .HasColumnType("integer");

                    b.Property<int?>("WhereFromId")
                        .HasColumnType("integer");

                    b.Property<string>("Work")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CurrentLocationId");

                    b.HasIndex("UserId");

                    b.HasIndex("WhereFromId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ButaAPI.Database.Model.Blog", b =>
                {
                    b.HasOne("ButaAPI.Database.Model.Location", "Location")
                        .WithMany()
                        .HasForeignKey("LocationId");

                    b.HasOne("ButaAPI.Database.Model.User", "Owner")
                        .WithMany("Blogs")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Location");

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("ButaAPI.Database.Model.Comment", b =>
                {
                    b.HasOne("ButaAPI.Database.Model.Blog", "Blog")
                        .WithMany("Commets")
                        .HasForeignKey("BlogId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ButaAPI.Database.Model.User", "Owner")
                        .WithMany()
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Blog");

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("ButaAPI.Database.Model.FriendshipRequest", b =>
                {
                    b.HasOne("ButaAPI.Database.Model.User", "Sender")
                        .WithMany("FriendshipRequests")
                        .HasForeignKey("SenderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Sender");
                });

            modelBuilder.Entity("ButaAPI.Database.Model.Like", b =>
                {
                    b.HasOne("ButaAPI.Database.Model.Blog", "Blog")
                        .WithMany("Likes")
                        .HasForeignKey("BlogId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ButaAPI.Database.Model.User", "Owner")
                        .WithMany()
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Blog");

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("ButaAPI.Database.Model.Message", b =>
                {
                    b.HasOne("ButaAPI.Database.Model.User", "Receiver")
                        .WithMany("Messages")
                        .HasForeignKey("ReceiverId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Receiver");
                });

            modelBuilder.Entity("ButaAPI.Database.Model.Notifications", b =>
                {
                    b.HasOne("ButaAPI.Database.Model.User", "Owner")
                        .WithMany("Notifications")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("ButaAPI.Database.Model.User", b =>
                {
                    b.HasOne("ButaAPI.Database.Model.Location", "CurrentLocation")
                        .WithMany()
                        .HasForeignKey("CurrentLocationId");

                    b.HasOne("ButaAPI.Database.Model.User", null)
                        .WithMany("Friends")
                        .HasForeignKey("UserId");

                    b.HasOne("ButaAPI.Database.Model.Location", "WhereFrom")
                        .WithMany()
                        .HasForeignKey("WhereFromId");

                    b.Navigation("CurrentLocation");

                    b.Navigation("WhereFrom");
                });

            modelBuilder.Entity("ButaAPI.Database.Model.Blog", b =>
                {
                    b.Navigation("Commets");

                    b.Navigation("Likes");
                });

            modelBuilder.Entity("ButaAPI.Database.Model.User", b =>
                {
                    b.Navigation("Blogs");

                    b.Navigation("Friends");

                    b.Navigation("FriendshipRequests");

                    b.Navigation("Messages");

                    b.Navigation("Notifications");
                });
#pragma warning restore 612, 618
        }
    }
}
