﻿// <auto-generated />
using System;
using HRJ.LMS.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace HRJ.LMS.Persistence.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20210720121237_002_LeadUpdate")]
    partial class _002_LeadUpdate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("HRJ.LMS.Domain.AppUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<bool>("ChangePassword")
                        .HasColumnType("bit");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FullName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("RefreshToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("RefreshTokenExpiry")
                        .HasColumnType("datetime2");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("UserType")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("HRJ.LMS.Domain.AppUserMenu", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AppUserRoleId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("MenuIcon")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MenuName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RouterLink")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AppUserRoleId");

                    b.ToTable("AppUserMenus");
                });

            modelBuilder.Entity("HRJ.LMS.Domain.AppUserRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("HRJ.LMS.Domain.AppUserState", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AppUserId")
                        .HasColumnType("nvarchar(450)");

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
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ExperienceCenterId")
                        .HasColumnType("int");

                    b.Property<string>("MobileNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ExperienceCenterId");

                    b.ToTable("ECContactDetails");
                });

            modelBuilder.Entity("HRJ.LMS.Domain.ECStateMapping", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

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
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ExpericenceCenterName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Pincode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("State")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ExperienceCenters");
                });

            modelBuilder.Entity("HRJ.LMS.Domain.Lead", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("AssignedToECId")
                        .HasColumnType("int");

                    b.Property<string>("AssignedToSalesPersonId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CalledById")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CallerRemarks")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Company")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ContactPersonName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DealerCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DealerName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ECRemarks")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EnquiryType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FutureRequirement")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FutureRequirementTileSegment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FutureRequirementVolume")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("LastUpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastUpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("LeadCallingStatusId")
                        .HasColumnType("int");

                    b.Property<int?>("LeadClassificationId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("LeadConversion")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("LeadDateTime")
                        .HasColumnType("datetime2");

                    b.Property<int?>("LeadEnquiryTypeId")
                        .HasColumnType("int");

                    b.Property<string>("LeadSource")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("LeadSourceId")
                        .HasColumnType("bigint");

                    b.Property<int?>("LeadSpaceTypeId")
                        .HasColumnType("int");

                    b.Property<int?>("LeadStatusId")
                        .HasColumnType("int");

                    b.Property<string>("LeadValueINR")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("QuantityInSquareFeet")
                        .HasColumnType("int");

                    b.Property<string>("SalesPersonRemarks")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("State")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Subject")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("VolumeInSqureFeet")
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
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CallingStatus")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RowOrder")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("LeadCallingStatuses");
                });

            modelBuilder.Entity("HRJ.LMS.Domain.LeadClassification", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Classification")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RowOrder")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("LeadClassifications");
                });

            modelBuilder.Entity("HRJ.LMS.Domain.LeadContactDetail", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ContactType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EmailAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("LeadId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("MobileNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("LeadId");

                    b.ToTable("LeadContactDetails");
                });

            modelBuilder.Entity("HRJ.LMS.Domain.LeadEnquiryType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("EnquiryType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RowOrder")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("LeadEnquiryTypes");
                });

            modelBuilder.Entity("HRJ.LMS.Domain.LeadEventStore", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ActionTakenById")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("ActionTakenOn")
                        .HasColumnType("datetime2");

                    b.Property<int>("IsEventToDisplay")
                        .HasColumnType("int");

                    b.Property<Guid?>("LeadId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("LeadStatusId")
                        .HasColumnType("int");

                    b.Property<string>("Remarks")
                        .HasColumnType("nvarchar(max)");

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
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("RowOrder")
                        .HasColumnType("int");

                    b.Property<string>("SpaceType")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("LeadSpaceTypes");
                });

            modelBuilder.Entity("HRJ.LMS.Domain.LeadStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("RowOrder")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("LeadStatuses");
                });

            modelBuilder.Entity("HRJ.LMS.Domain.State", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("LastUpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastUpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Region")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StateName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("States");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

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
                        .WithMany("ECContactDetails")
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
