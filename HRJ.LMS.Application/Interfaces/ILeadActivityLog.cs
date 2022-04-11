using System.Collections.Generic;
using System.Threading.Tasks;
using HRJ.LMS.Domain;

namespace HRJ.LMS.Application.Interfaces
{
    public interface ILeadActivityLog
    {
        Task AddLeadActivityLog(LeadActivity leadActivity);

        Task AddRangeLeadActivityLog(List<LeadActivity> leadActivities);

        Task AddLeadActivityLog(Lead lead, string userName, string heading, string leadActivityRemarks, int isEventToDisplay, AppUser appUser);

        Task AddRangeLeadActivityLog(List<Lead> leads, string userName, string heading, string leadActivityRemarks);
    }
}