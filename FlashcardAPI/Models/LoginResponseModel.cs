using FlashcardAPI.Data;

namespace FlashcardAPI.Models
{
    public class LoginResponseModel
    {
        public bool IsLoggedIn { get; set; }
        public bool Status { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; } = null;
        public User UserData { get; set; }
    }
}
