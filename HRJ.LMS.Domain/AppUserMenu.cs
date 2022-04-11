namespace HRJ.LMS.Domain
{
    public class AppUserMenu
    {
        public int Id { get; set; }
        public string MenuName { get; set; }
        public string MenuIcon { get; set; }
        public string RouterLink { get; set; }
        public AppUserRole AppUserRole { get; set; }
        public int RowOrder { get; set; }
    }
}