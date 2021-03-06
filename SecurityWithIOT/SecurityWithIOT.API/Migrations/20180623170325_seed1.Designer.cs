﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using SecurityWithIOT.API.Data;
using System;

namespace SecurityWithIOT.API.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20180623170325_seed1")]
    partial class seed1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.3-rtm-10026")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SecurityWithIOT.API.Model.Address", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CityId");

                    b.Property<int>("CountryId");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("CreatedUser");

                    b.Property<string>("Description");

                    b.Property<int>("DistrictId");

                    b.Property<bool>("IsDelete");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<string>("ModifiedUser");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.HasIndex("CountryId");

                    b.HasIndex("DistrictId");

                    b.HasIndex("UserId");

                    b.ToTable("Address");
                });

            modelBuilder.Entity("SecurityWithIOT.API.Model.City", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CityName");

                    b.Property<int>("CountryId");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("CreatedUser");

                    b.Property<bool>("IsDelete");

                    b.Property<double>("Latitude");

                    b.Property<double>("Longitude");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<string>("ModifiedUser");

                    b.Property<long>("Population");

                    b.Property<string>("Region");

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.ToTable("City");
                });

            modelBuilder.Entity("SecurityWithIOT.API.Model.Company", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CompanyName");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("CreatedUser");

                    b.Property<string>("Fax");

                    b.Property<bool>("IsDelete");

                    b.Property<string>("Mail");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<string>("ModifiedUser");

                    b.Property<string>("Tel");

                    b.HasKey("Id");

                    b.ToTable("Company");
                });

            modelBuilder.Entity("SecurityWithIOT.API.Model.Country", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CountryCode");

                    b.Property<string>("CountryName");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("CreatedUser");

                    b.Property<bool>("IsDelete");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<string>("ModifiedUser");

                    b.HasKey("Id");

                    b.ToTable("Countries");
                });

            modelBuilder.Entity("SecurityWithIOT.API.Model.Department", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CompanyId");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("CreatedUser");

                    b.Property<string>("DepartmentName");

                    b.Property<string>("Fax");

                    b.Property<bool>("IsDelete");

                    b.Property<string>("Mail");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<string>("ModifiedUser");

                    b.Property<string>("Tel");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("SecurityWithIOT.API.Model.District", b =>
                {
                    b.Property<int>("Id");

                    b.Property<int>("CityId");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("CreatedUser");

                    b.Property<string>("DistrictName");

                    b.Property<bool>("IsDelete");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<string>("ModifiedUser");

                    b.HasKey("Id");

                    b.ToTable("District");
                });

            modelBuilder.Entity("SecurityWithIOT.API.Model.Photo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("CreatedUser");

                    b.Property<DateTime>("DateAdded");

                    b.Property<string>("Description");

                    b.Property<bool>("IsDelete");

                    b.Property<bool>("IsMain");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<string>("ModifiedUser");

                    b.Property<string>("Url");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Photo");
                });

            modelBuilder.Entity("SecurityWithIOT.API.Model.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("CompanyId");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("CreatedUser");

                    b.Property<DateTime>("DateOfBirth");

                    b.Property<int?>("DepartmentId");

                    b.Property<string>("Email");

                    b.Property<string>("Gender");

                    b.Property<string>("Guid");

                    b.Property<string>("Introduction");

                    b.Property<bool>("IsDelete");

                    b.Property<string>("KnownAs");

                    b.Property<DateTime>("LastEnterance");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<string>("ModifiedUser");

                    b.Property<byte[]>("PasswordHash");

                    b.Property<byte[]>("PasswordSalt");

                    b.Property<string>("Phone");

                    b.Property<string>("Title");

                    b.Property<string>("Username");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.HasIndex("DepartmentId");

                    b.HasIndex("Email")
                        .IsUnique()
                        .HasFilter("[Email] IS NOT NULL");

                    b.ToTable("User");
                });

            modelBuilder.Entity("SecurityWithIOT.API.Model.Address", b =>
                {
                    b.HasOne("SecurityWithIOT.API.Model.City", "City")
                        .WithMany()
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SecurityWithIOT.API.Model.Country", "Country")
                        .WithMany()
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SecurityWithIOT.API.Model.District", "District")
                        .WithMany()
                        .HasForeignKey("DistrictId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SecurityWithIOT.API.Model.User", "User")
                        .WithMany("Addresses")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SecurityWithIOT.API.Model.City", b =>
                {
                    b.HasOne("SecurityWithIOT.API.Model.Country", "Country")
                        .WithMany("Cities")
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("SecurityWithIOT.API.Model.Department", b =>
                {
                    b.HasOne("SecurityWithIOT.API.Model.Company", "Company")
                        .WithMany("Departments")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("SecurityWithIOT.API.Model.District", b =>
                {
                    b.HasOne("SecurityWithIOT.API.Model.City", "City")
                        .WithMany("Districts")
                        .HasForeignKey("Id")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("SecurityWithIOT.API.Model.Photo", b =>
                {
                    b.HasOne("SecurityWithIOT.API.Model.User", "User")
                        .WithMany("Photos")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SecurityWithIOT.API.Model.User", b =>
                {
                    b.HasOne("SecurityWithIOT.API.Model.Company", "Company")
                        .WithMany("Users")
                        .HasForeignKey("CompanyId");

                    b.HasOne("SecurityWithIOT.API.Model.Department", "Department")
                        .WithMany("Users")
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Restrict);
                });
#pragma warning restore 612, 618
        }
    }
}
