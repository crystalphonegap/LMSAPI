﻿// <auto-generated />
using System;
using HRJ.LMS.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace HRJ.LMS.Persistence.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("HRJ.LMS.Domain.AppUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<bool>("ChangePassword")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Email")
                        .HasColumnType("varchar(256) CHARACTER SET utf8mb4")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("FullName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("varchar(256) CHARACTER SET utf8mb4")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("varchar(256) CHARACTER SET utf8mb4")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("RefreshToken")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime?>("RefreshTokenExpiry")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("UserName")
                        .HasColumnType("varchar(256) CHARACTER SET utf8mb4")
                        .HasMaxLength(256);

                    b.Property<string>("UserType")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("HRJ.LMS.Domain.AppUserMenu", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("AppUserRoleId")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("MenuIcon")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("MenuName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("RouterLink")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.HasIndex("AppUserRoleId");

                    b.ToTable("AppUserMenus");
                });

            modelBuilder.Entity("HRJ.LMS.Domain.AppUserRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Description")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(256) CHARACTER SET utf8mb4")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasColumnType("varchar(256) CHARACTER SET utf8mb4")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("HRJ.LMS.Domain.AppUserState", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("AppUserId")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<int?>("StateId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AppUserId");

                    b.HasIndex("StateId");

                    b.ToTable("AppUserStates");
                });

            modelBuilder.Entity("HRJ.LMS.Domain.ECContactDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("ExperienceCenterId")
                        .HasColumnType("int");

                    b.Property<string>("MobileNumber")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.HasIndex("ExperienceCenterId");

                    b.ToTable("ECContactDetails");
                });

            modelBuilder.Entity("HRJ.LMS.Domain.ECStateMapping", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("ExperienceCenterId")
                        .HasColumnType("int");

                    b.Property<int?>("StateId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ExperienceCenterId");

                    b.HasIndex("StateId");

                    b.ToTable("ECStateMappings");
                });

            modelBuilder.Entity("HRJ.LMS.Domain.ExperienceCenter", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Address")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("City")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("ExpericenceCenterName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Pincode")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("State")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("ExperienceCenters");
                });

            modelBuilder.Entity("HRJ.LMS.Domain.Lead", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Address")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int?>("AssignedToECId")
                        .HasColumnType("int");

                    b.Property<string>("AssignedToSalesPersonId")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("CalledById")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("CallerRemarks")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("City")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Company")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("ContactPersonName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("DealerCode")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("DealerName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Description")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("ECRemarks")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("EnquiryType")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("FutureRequirement")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("FutureRequirementTileSegment")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("FutureRequirementVolume")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime>("LastUpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("LastUpdatedBy")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int?>("LeadCallingStatusId")
                        .HasColumnType("int");

                    b.Property<int?>("LeadClassificationId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("LeadConversion")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("LeadDateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("LeadEnquiryTypeId")
                        .HasColumnType("int");

                    b.Property<string>("LeadSource")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<long>("LeadSourceId")
                        .HasColumnType("bigint");

                    b.Property<int?>("LeadSpaceTypeId")
                        .HasColumnType("int");

                    b.Property<int?>("LeadStatusId")
                        .HasColumnType("int");

                    b.Property<string>("LeadValueINR")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("QuantityInSquareFeet")
                        .HasColumnType("int");

                    b.Property<string>("SalesPersonRemarks")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("State")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Subject")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("VolumeInSqureFeet")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AssignedToECId");

                    b.HasIndex("AssignedToSalesPersonId");

                    b.HasIndex("CalledById");

                    b.HasIndex("LeadCallingStatusId");

                    b.HasIndex("LeadClassificationId");

                    b.HasIndex("LeadEnquiryTypeId");

                    b.HasIndex("LeadSpaceTypeId");

                    b.HasIndex("LeadStatusId");

                    b.ToTable("Leads");
                });

            modelBuilder.Entity("HRJ.LMS.Domain.LeadCallingStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("CallingStatus")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("RowOrder")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("LeadCallingStatuses");
                });

            modelBuilder.Entity("HRJ.LMS.Domain.LeadClassification", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Classification")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("RowOrder")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("LeadClassifications");
                });

            modelBuilder.Entity("HRJ.LMS.Domain.LeadContactDetail", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("ContactType")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("EmailAddress")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<Guid?>("LeadId")
                        .HasColumnType("char(36)");

                    b.Property<string>("MobileNumber")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.HasIndex("LeadId");

                    b.ToTable("LeadContactDetails");
                });

            modelBuilder.Entity("HRJ.LMS.Domain.LeadEnquiryType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("EnquiryType")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("RowOrder")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("LeadEnquiryTypes");
                });

            modelBuilder.Entity("HRJ.LMS.Domain.LeadEventStore", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("ActionTakenById")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<DateTime>("ActionTakenOn")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("IsEventToDisplay")
                        .HasColumnType("int");

                    b.Property<Guid?>("LeadId")
                        .HasColumnType("char(36)");

                    b.Property<int?>("LeadStatusId")
                        .HasColumnType("int");

                    b.Property<string>("Remarks")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.HasIndex("ActionTakenById");

                    b.HasIndex("LeadId");

                    b.HasIndex("LeadStatusId");

                    b.ToTable("LeadEventStores");
                });

            modelBuilder.Entity("HRJ.LMS.Domain.LeadSpaceType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("RowOrder")
                        .HasColumnType("int");

                    b.Property<string>("SpaceType")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("LeadSpaceTypes");
                });

            modelBuilder.Entity("HRJ.LMS.Domain.LeadStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("RowOrder")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("LeadStatuses");
                });

            modelBuilder.Entity("HRJ.LMS.Domain.State", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("LastUpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("LastUpdatedBy")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Region")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("StateName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("States");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("RoleId")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("Value")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("HRJ.LMS.Domain.AppUserMenu", b =>
                {
                    b.HasOne("HRJ.LMS.Domain.AppUserRole", "AppUserRole")
                        .WithMany()
                        .HasForeignKey("AppUserRoleId");
                });

            modelBuilder.Entity("HRJ.LMS.Domain.AppUserState", b =>
                {
                    b.HasOne("HRJ.LMS.Domain.AppUser", "AppUser")
                        .WithMany()
                        .HasForeignKey("AppUserId");

                    b.HasOne("HRJ.LMS.Domain.State", "State")
                        .WithMany()
                        .HasForeignKey("StateId");
                });

            modelBuilder.Entity("HRJ.LMS.Domain.ECContactDetail", b =>
                {
                    b.HasOne("HRJ.LMS.Domain.ExperienceCenter", null)
                        .WithMany("ExperienceCenterContactDetails")
                        .HasForeignKey("ExperienceCenterId");
                });

            modelBuilder.Entity("HRJ.LMS.Domain.ECStateMapping", b =>
                {
                    b.HasOne("HRJ.LMS.Domain.ExperienceCenter", "ExperienceCenter")
                        .WithMany()
                        .HasForeignKey("ExperienceCenterId");

                    b.HasOne("HRJ.LMS.Domain.State", "State")
                        .WithMany()
                        .HasForeignKey("StateId");
                });

            modelBuilder.Entity("HRJ.LMS.Domain.Lead", b =>
                {
                    b.HasOne("HRJ.LMS.Domain.ExperienceCenter", "AssignedToEC")
                        .WithMany()
                        .HasForeignKey("AssignedToECId");

                    b.HasOne("HRJ.LMS.Domain.AppUser", "AssignedToSalesPerson")
                        .WithMany()
                        .HasForeignKey("AssignedToSalesPersonId");

                    b.HasOne("HRJ.LMS.Domain.AppUser", "CalledBy")
                        .WithMany()
                        .HasForeignKey("CalledById");

                    b.HasOne("HRJ.LMS.Domain.LeadCallingStatus", "LeadCallingStatus")
                        .WithMany()
                        .HasForeignKey("LeadCallingStatusId");

                    b.HasOne("HRJ.LMS.Domain.LeadClassification", "LeadClassification")
                        .WithMany()
                        .HasForeignKey("LeadClassificationId");

                    b.HasOne("HRJ.LMS.Domain.LeadEnquiryType", "LeadEnquiryType")
                        .WithMany()
                        .HasForeignKey("LeadEnquiryTypeId");

                    b.HasOne("HRJ.LMS.Domain.LeadSpaceType", "LeadSpaceType")
                        .WithMany()
                        .HasForeignKey("LeadSpaceTypeId");

                    b.HasOne("HRJ.LMS.Domain.LeadStatus", "LeadStatus")
                        .WithMany()
                        .HasForeignKey("LeadStatusId");
                });

            modelBuilder.Entity("HRJ.LMS.Domain.LeadContactDetail", b =>
                {
                    b.HasOne("HRJ.LMS.Domain.Lead", null)
                        .WithMany("LeadContactDetails")
                        .HasForeignKey("LeadId");
                });

            modelBuilder.Entity("HRJ.LMS.Domain.LeadEventStore", b =>
                {
                    b.HasOne("HRJ.LMS.Domain.AppUser", "ActionTakenBy")
                        .WithMany()
                        .HasForeignKey("ActionTakenById");

                    b.HasOne("HRJ.LMS.Domain.Lead", "Lead")
                        .WithMany()
                        .HasForeignKey("LeadId");

                    b.HasOne("HRJ.LMS.Domain.LeadStatus", "LeadStatus")
                        .WithMany()
                        .HasForeignKey("LeadStatusId");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("HRJ.LMS.Domain.AppUserRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("HRJ.LMS.Domain.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("HRJ.LMS.Domain.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("HRJ.LMS.Domain.AppUserRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HRJ.LMS.Domain.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("HRJ.LMS.Domain.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
