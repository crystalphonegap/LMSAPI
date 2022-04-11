namespace HRJ.LMS.Application.Dto.User
{
    public class UserDto
    {
        public string FullName { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public string UserName { get; set; }
        public bool ChangePassword { get; set; }
    }
}