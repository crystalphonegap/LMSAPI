using MediatR;

namespace HRJ.LMS.Application.Dto
{
    public class BaseDto
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public Unit Unit { get; set; }
    }
}