using Yummi.Application.Enum;

namespace Yummi.Application.Models
{
    public class Error
    {
        public ErrorCode Code { get; set; }
        public string Message { get; set; } = null!;
    }
}
