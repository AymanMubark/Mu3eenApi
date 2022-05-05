﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Mu3een.Data;

#nullable disable

namespace Mu3een.Migrations
{
    [DbContext(typeof(Mu3eenContext))]
    partial class Mu3eenContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Mu3een.Entities.Region", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NameAr")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("UpdateAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Regions");
                });

            modelBuilder.Entity("Mu3een.Entities.Reward", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Content")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("InstitutionId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("InstitutionId");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Numbers")
                        .HasColumnType("int");

                    b.Property<int>("Points")
                        .HasColumnType("int");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("UpdateAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("InstitutionId");

                    b.ToTable("Rewards");
                });

            modelBuilder.Entity("Mu3een.Entities.SocialEvent", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ExpiryDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("InstitutionId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("InstitutionId");

                    b.Property<double?>("Latitude")
                        .HasColumnType("float");

                    b.Property<double?>("Longitude")
                        .HasColumnType("float");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Points")
                        .HasColumnType("int");

                    b.Property<Guid?>("RegionId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("RegionId");

                    b.Property<Guid?>("SocialEventTypeId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("SocialEventTypeId");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("UpdateAt")
                        .HasColumnType("datetime2");

                    b.Property<int?>("VolunteerRequried")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("InstitutionId");

                    b.HasIndex("RegionId");

                    b.HasIndex("SocialEventTypeId");

                    b.ToTable("SocialEvents");
                });

            modelBuilder.Entity("Mu3een.Entities.SocialEventType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NameAr")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("UpdateAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("SocialEventTypes");
                });

            modelBuilder.Entity("Mu3een.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("UpdateAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasDiscriminator<string>("Discriminator").HasValue("User");
                });

            modelBuilder.Entity("Mu3een.Entities.VolunteerReward", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("RewardId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("RewardId");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("UpdateAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("VolunteerId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("VolunteerId");

                    b.HasKey("Id");

                    b.HasIndex("RewardId");

                    b.HasIndex("VolunteerId");

                    b.ToTable("VolunteerRewards");
                });

            modelBuilder.Entity("Mu3een.Entities.VolunteerSocialEvent", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Completed")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("SocialEventId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("SocialEventId");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("UpdateAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("VolunteerId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("VolunteerId");

                    b.HasKey("Id");

                    b.HasIndex("SocialEventId");

                    b.HasIndex("VolunteerId");

                    b.ToTable("VolunteerSocialEvents");
                });

            modelBuilder.Entity("Mu3een.Entities.Admin", b =>
                {
                    b.HasBaseType("Mu3een.Entities.User");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Admin_Password");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasDiscriminator().HasValue("Admin");
                });

            modelBuilder.Entity("Mu3een.Entities.Institution", b =>
                {
                    b.HasBaseType("Mu3een.Entities.User");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.HasDiscriminator().HasValue("Institution");
                });

            modelBuilder.Entity("Mu3een.Entities.Volunteer", b =>
                {
                    b.HasBaseType("Mu3een.Entities.User");

                    b.Property<int?>("Age")
                        .HasColumnType("int");

                    b.Property<int?>("Gender")
                        .HasColumnType("int");

                    b.Property<string>("OTP")
                        .HasMaxLength(4)
                        .HasColumnType("nvarchar(4)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Volunteer_Phone");

                    b.Property<int>("Points")
                        .HasColumnType("int");

                    b.HasDiscriminator().HasValue("Volunteer");
                });

            modelBuilder.Entity("Mu3een.Entities.Reward", b =>
                {
                    b.HasOne("Mu3een.Entities.Institution", "Institution")
                        .WithMany()
                        .HasForeignKey("InstitutionId");

                    b.Navigation("Institution");
                });

            modelBuilder.Entity("Mu3een.Entities.SocialEvent", b =>
                {
                    b.HasOne("Mu3een.Entities.Institution", "Institution")
                        .WithMany("SocialEvents")
                        .HasForeignKey("InstitutionId");

                    b.HasOne("Mu3een.Entities.Region", "Region")
                        .WithMany()
                        .HasForeignKey("RegionId");

                    b.HasOne("Mu3een.Entities.SocialEventType", "SocialEventType")
                        .WithMany()
                        .HasForeignKey("SocialEventTypeId");

                    b.Navigation("Institution");

                    b.Navigation("Region");

                    b.Navigation("SocialEventType");
                });

            modelBuilder.Entity("Mu3een.Entities.VolunteerReward", b =>
                {
                    b.HasOne("Mu3een.Entities.Reward", "Reward")
                        .WithMany()
                        .HasForeignKey("RewardId");

                    b.HasOne("Mu3een.Entities.Volunteer", "Volunteer")
                        .WithMany("Rewards")
                        .HasForeignKey("VolunteerId");

                    b.Navigation("Reward");

                    b.Navigation("Volunteer");
                });

            modelBuilder.Entity("Mu3een.Entities.VolunteerSocialEvent", b =>
                {
                    b.HasOne("Mu3een.Entities.SocialEvent", "SocialEvent")
                        .WithMany()
                        .HasForeignKey("SocialEventId");

                    b.HasOne("Mu3een.Entities.Volunteer", "Volunteer")
                        .WithMany("Services")
                        .HasForeignKey("VolunteerId");

                    b.Navigation("SocialEvent");

                    b.Navigation("Volunteer");
                });

            modelBuilder.Entity("Mu3een.Entities.Institution", b =>
                {
                    b.Navigation("SocialEvents");
                });

            modelBuilder.Entity("Mu3een.Entities.Volunteer", b =>
                {
                    b.Navigation("Rewards");

                    b.Navigation("Services");
                });
#pragma warning restore 612, 618
        }
    }
}
