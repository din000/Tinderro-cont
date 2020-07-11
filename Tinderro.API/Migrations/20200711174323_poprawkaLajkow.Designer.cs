﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Tinderro.API.Data;

namespace Tinderro.API.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20200711174323_poprawkaLajkow")]
    partial class poprawkaLajkow
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Tinderro.API.Models.Like", b =>
                {
                    b.Property<int>("UserLikesId")
                        .HasColumnType("int");

                    b.Property<int>("SomeoneLikesMeId")
                        .HasColumnType("int");

                    b.HasKey("UserLikesId", "SomeoneLikesMeId");

                    b.HasIndex("SomeoneLikesMeId");

                    b.ToTable("Likes");
                });

            modelBuilder.Entity("Tinderro.API.Models.Photo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateAdded")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsMain")
                        .HasColumnType("bit");

                    b.Property<string>("Public_id")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Url")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("photos");
                });

            modelBuilder.Entity("Tinderro.API.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Children")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Education")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EyeColor")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FreeTime")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FriendeWouldDescribeMe")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Gender")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Growth")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HairColor")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ILike")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IdoNotLike")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Interests")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ItFeelsBestIn")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Languages")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("LastActive")
                        .HasColumnType("datetime2");

                    b.Property<string>("LookingFor")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MakesMeLaugh")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MartialStatus")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Motto")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Movies")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Music")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("PasswordHash")
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Personality")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Profession")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Sport")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ZodiacSign")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("users");
                });

            modelBuilder.Entity("Tinderro.API.Models.Like", b =>
                {
                    b.HasOne("Tinderro.API.Models.User", "SomeoneLikes")
                        .WithMany("SomeoneLikes")
                        .HasForeignKey("SomeoneLikesMeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Tinderro.API.Models.User", "UserLikes")
                        .WithMany("UserLikes")
                        .HasForeignKey("UserLikesId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("Tinderro.API.Models.Photo", b =>
                {
                    b.HasOne("Tinderro.API.Models.User", "User")
                        .WithMany("Photos")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
