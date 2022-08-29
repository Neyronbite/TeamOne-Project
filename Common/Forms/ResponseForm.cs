using Common.Enums;
using Common.Models;

namespace Common.Forms
{
    public class ResponseForm
    {
        public ResponseCodes ResponseCode { get; set; }
        public string Description { get; set; } = string.Empty;
        public User? User { get; set; }
    }
}
