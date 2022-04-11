using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HRJ.LMS.Persistence.Migrations
{
    public partial class _001_LeadEntitiesAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    FullName = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    UserType = table.Column<string>(nullable: true),
                    ChangePassword = table.Column<bool>(nullable: false),
                    RefreshToken = table.Column<string>(nullable: true),
                    RefreshTokenExpiry = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExperienceCenters",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ExperienceCenterName = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    State = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    Pincode = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExperienceCenters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LeadCallingStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CallingStatus = table.Column<string>(nullable: true),
                    RowOrder = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeadCallingStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LeadClassifications",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Classification = table.Column<string>(nullable: true),
                    RowOrder = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeadClassifications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LeadEnquiryTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    EnquiryType = table.Column<string>(nullable: true),
                    RowOrder = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeadEnquiryTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LeadReminderOptions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Reminder = table.Column<string>(nullable: true),
                    Duration = table.Column<int>(nullable: false),
                    DurationType = table.Column<int>(nullable: false),
                    DurationTypeInString = table.Column<string>(nullable: true),
                    RowOrder = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeadReminderOptions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LeadSpaceTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    SpaceType = table.Column<string>(nullable: true),
                    RowOrder = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeadSpaceTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LeadStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Status = table.Column<string>(nullable: true),
                    RowOrder = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeadStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "States",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    StateName = table.Column<string>(nullable: true),
                    Region = table.Column<string>(nullable: true),
                    LastUpdatedBy = table.Column<string>(nullable: true),
                    LastUpdatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_States", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppUserMenus",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    MenuName = table.Column<string>(nullable: true),
                    MenuIcon = table.Column<string>(nullable: true),
                    RouterLink = table.Column<string>(nullable: true),
                    AppUserRoleId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserMenus", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppUserMenus_AspNetRoles_AppUserRoleId",
                        column: x => x.AppUserRoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ECContactDetails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    MobileNumber = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    ExperienceCenterId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ECContactDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ECContactDetails_ExperienceCenters_ExperienceCenterId",
                        column: x => x.ExperienceCenterId,
                        principalTable: "ExperienceCenters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Leads",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    LeadDateTime = table.Column<DateTime>(nullable: false),
                    EnquiryType = table.Column<string>(nullable: true),
                    ContactPersonName = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    State = table.Column<string>(nullable: true),
                    Company = table.Column<string>(nullable: true),
                    Subject = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    LeadCallingStatusId = table.Column<int>(nullable: true),
                    CalledById = table.Column<string>(nullable: true),
                    LeadClassificationId = table.Column<int>(nullable: true),
                    LeadStatusId = table.Column<int>(nullable: true),
                    QuantityInSquareFeet = table.Column<int>(nullable: true),
                    LeadEnquiryTypeId = table.Column<int>(nullable: true),
                    LeadSpaceTypeId = table.Column<int>(nullable: true),
                    AssignedToECId = table.Column<int>(nullable: true),
                    ECRemarks = table.Column<string>(nullable: true),
                    AssignedToSalesPersonId = table.Column<string>(nullable: true),
                    SalesPersonRemarks = table.Column<string>(nullable: true),
                    LeadConversion = table.Column<DateTime>(nullable: true),
                    DealerName = table.Column<string>(nullable: true),
                    DealerCode = table.Column<string>(nullable: true),
                    LeadValueINR = table.Column<string>(nullable: true),
                    VolumeInSqureFeet = table.Column<int>(nullable: true),
                    FutureRequirement = table.Column<string>(nullable: true),
                    FutureRequirementTileSegment = table.Column<string>(nullable: true),
                    FutureRequirementVolume = table.Column<string>(nullable: true),
                    LeadSource = table.Column<string>(nullable: true),
                    LeadSourceId = table.Column<long>(nullable: false),
                    LastUpdatedBy = table.Column<string>(nullable: true),
                    LastUpdatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Leads", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Leads_ExperienceCenters_AssignedToECId",
                        column: x => x.AssignedToECId,
                        principalTable: "ExperienceCenters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Leads_AspNetUsers_AssignedToSalesPersonId",
                        column: x => x.AssignedToSalesPersonId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Leads_AspNetUsers_CalledById",
                        column: x => x.CalledById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Leads_LeadCallingStatuses_LeadCallingStatusId",
                        column: x => x.LeadCallingStatusId,
                        principalTable: "LeadCallingStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Leads_LeadClassifications_LeadClassificationId",
                        column: x => x.LeadClassificationId,
                        principalTable: "LeadClassifications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Leads_LeadEnquiryTypes_LeadEnquiryTypeId",
                        column: x => x.LeadEnquiryTypeId,
                        principalTable: "LeadEnquiryTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Leads_LeadSpaceTypes_LeadSpaceTypeId",
                        column: x => x.LeadSpaceTypeId,
                        principalTable: "LeadSpaceTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Leads_LeadStatuses_LeadStatusId",
                        column: x => x.LeadStatusId,
                        principalTable: "LeadStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AppUserStates",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AppUserId = table.Column<string>(nullable: true),
                    StateId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserStates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppUserStates_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AppUserStates_States_StateId",
                        column: x => x.StateId,
                        principalTable: "States",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ECStateMappings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ExperienceCenterId = table.Column<int>(nullable: true),
                    StateId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ECStateMappings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ECStateMappings_ExperienceCenters_ExperienceCenterId",
                        column: x => x.ExperienceCenterId,
                        principalTable: "ExperienceCenters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ECStateMappings_States_StateId",
                        column: x => x.StateId,
                        principalTable: "States",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LeadCallerRemark",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    LeadId = table.Column<Guid>(nullable: false),
                    CallerRemark = table.Column<string>(nullable: true),
                    CallerRemarkAt = table.Column<DateTime>(nullable: false),
                    CallerRemarkBy = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeadCallerRemark", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LeadCallerRemark_Leads_LeadId",
                        column: x => x.LeadId,
                        principalTable: "Leads",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LeadContactDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    MobileNumber = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    EmailAddress = table.Column<string>(nullable: true),
                    ContactType = table.Column<string>(nullable: true),
                    LeadId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeadContactDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LeadContactDetails_Leads_LeadId",
                        column: x => x.LeadId,
                        principalTable: "Leads",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LeadEventStores",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    LeadId = table.Column<Guid>(nullable: true),
                    LeadStatusId = table.Column<int>(nullable: true),
                    ActionTakenById = table.Column<string>(nullable: true),
                    ActionTakenOn = table.Column<DateTime>(nullable: false),
                    Remarks = table.Column<string>(nullable: true),
                    IsEventToDisplay = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeadEventStores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LeadEventStores_AspNetUsers_ActionTakenById",
                        column: x => x.ActionTakenById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LeadEventStores_Leads_LeadId",
                        column: x => x.LeadId,
                        principalTable: "Leads",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LeadEventStores_LeadStatuses_LeadStatusId",
                        column: x => x.LeadStatusId,
                        principalTable: "LeadStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppUserMenus_AppUserRoleId",
                table: "AppUserMenus",
                column: "AppUserRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AppUserStates_AppUserId",
                table: "AppUserStates",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AppUserStates_StateId",
                table: "AppUserStates",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ECContactDetails_ExperienceCenterId",
                table: "ECContactDetails",
                column: "ExperienceCenterId");

            migrationBuilder.CreateIndex(
                name: "IX_ECStateMappings_ExperienceCenterId",
                table: "ECStateMappings",
                column: "ExperienceCenterId");

            migrationBuilder.CreateIndex(
                name: "IX_ECStateMappings_StateId",
                table: "ECStateMappings",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_LeadCallerRemark_LeadId",
                table: "LeadCallerRemark",
                column: "LeadId");

            migrationBuilder.CreateIndex(
                name: "IX_LeadContactDetails_LeadId",
                table: "LeadContactDetails",
                column: "LeadId");

            migrationBuilder.CreateIndex(
                name: "IX_LeadEventStores_ActionTakenById",
                table: "LeadEventStores",
                column: "ActionTakenById");

            migrationBuilder.CreateIndex(
                name: "IX_LeadEventStores_LeadId",
                table: "LeadEventStores",
                column: "LeadId");

            migrationBuilder.CreateIndex(
                name: "IX_LeadEventStores_LeadStatusId",
                table: "LeadEventStores",
                column: "LeadStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Leads_AssignedToECId",
                table: "Leads",
                column: "AssignedToECId");

            migrationBuilder.CreateIndex(
                name: "IX_Leads_AssignedToSalesPersonId",
                table: "Leads",
                column: "AssignedToSalesPersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Leads_CalledById",
                table: "Leads",
                column: "CalledById");

            migrationBuilder.CreateIndex(
                name: "IX_Leads_LeadCallingStatusId",
                table: "Leads",
                column: "LeadCallingStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Leads_LeadClassificationId",
                table: "Leads",
                column: "LeadClassificationId");

            migrationBuilder.CreateIndex(
                name: "IX_Leads_LeadEnquiryTypeId",
                table: "Leads",
                column: "LeadEnquiryTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Leads_LeadSpaceTypeId",
                table: "Leads",
                column: "LeadSpaceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Leads_LeadStatusId",
                table: "Leads",
                column: "LeadStatusId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppUserMenus");

            migrationBuilder.DropTable(
                name: "AppUserStates");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "ECContactDetails");

            migrationBuilder.DropTable(
                name: "ECStateMappings");

            migrationBuilder.DropTable(
                name: "LeadCallerRemark");

            migrationBuilder.DropTable(
                name: "LeadContactDetails");

            migrationBuilder.DropTable(
                name: "LeadEventStores");

            migrationBuilder.DropTable(
                name: "LeadReminderOptions");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "States");

            migrationBuilder.DropTable(
                name: "Leads");

            migrationBuilder.DropTable(
                name: "ExperienceCenters");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "LeadCallingStatuses");

            migrationBuilder.DropTable(
                name: "LeadClassifications");

            migrationBuilder.DropTable(
                name: "LeadEnquiryTypes");

            migrationBuilder.DropTable(
                name: "LeadSpaceTypes");

            migrationBuilder.DropTable(
                name: "LeadStatuses");
        }
    }
}
