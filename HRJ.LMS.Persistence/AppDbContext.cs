using HRJ.LMS.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HRJ.LMS.Persistence
{
    public class AppDbContext : IdentityDbContext<AppUser, AppUserRole, string>
    {
        public AppDbContext(DbContextOptions options) : base (options)
        {
            
        }

        public DbSet<ExperienceCenter> ExperienceCenters { get; set; }
        public DbSet<ECContactDetail> ECContactDetails { get; set; }
        public DbSet<ECStateMapping> ECStateMappings { get; set; }
        public DbSet<Lead> Leads { get; set; }
        public DbSet<LeadCallingStatus> LeadCallingStatuses { get; set; }
        public DbSet<LeadClassification> LeadClassifications { get; set; }
        public DbSet<LeadContactDetail> LeadContactDetails { get; set; }
        public DbSet<LeadEnquiryType> LeadEnquiryTypes { get; set; }
        public DbSet<LeadActivity> LeadActivities { get; set; }
        public DbSet<LeadSpaceType> LeadSpaceTypes { get; set; }
        public DbSet<LeadStatus> LeadStatuses { get; set; }
        public DbSet<AppUserMenu> AppUserMenus { get; set; }
        public DbSet<LeadReminderOption> LeadReminderOptions { get; set; }
        public DbSet<LeadReminder> LeadReminders { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<AppUserState> AppUserStates { get; set; }
        public DbSet<AppUserExperienceCenter> AppUserExperienceCenters { get; set; }
        public DbSet<UploadExcelTemplate> UploadExcelTemplates { get; set; }
        public DbSet<UploadExcelLog> UploadExcelLogs { get; set; }
        public DbSet<LeadECManagerRemark> LeadECManagerRemarks { get; set; }
        public DbSet<LeadCallerRemark> LeadCallerRemarks { get; set; }
        public DbSet<LeadSource> LeadSources { get; set; }
        public DbSet<JustDialLead> JustDialLeads { get; set; }
        public DbSet<StateCityMapping> StateCityMappings { get; set; }
        public DbSet<FiscalYear> FiscalYears { get; set; }
        public DbSet<FiscalMonth> FiscalMonths { get; set; }
        public DbSet<WebLead> WebLeads { get; set; }
        public DbSet<ExcludeLead> ExcludeLeads { get; set; }
        public DbSet<LeadInvoiceFileDetail> LeadInvoiceFileDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            /* builder.Entity<Lead>()
                .Property(x => x.LastUpdatedAt)
                .HasDefaultValueSql("GetDate()"); */
        }
    }
}