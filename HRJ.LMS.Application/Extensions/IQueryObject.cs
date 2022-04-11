namespace HRJ.LMS.Application.Extensions
{
    public interface IQueryObject
    {
        int? PageNo { get; set; }
        int? PageSize { get; set; }
        string SortBy { get; set; }
        bool IsSortingAscending { get; set; }
    }
}