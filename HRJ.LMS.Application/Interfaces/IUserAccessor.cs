namespace HRJ.LMS.Application.Interfaces
{
    public interface IUserAccessor
    {
        string GetCurrentUserId();
        string GetCurrentUserRole();
        string GetCurrentUserName();
    }
}