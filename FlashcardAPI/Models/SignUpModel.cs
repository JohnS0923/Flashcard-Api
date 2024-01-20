namespace FlashcardAPI.Models
{
    public class SignUpModel
    {
        public bool Status { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; } = null;
        public int UserId { get; set; }
    }
}
