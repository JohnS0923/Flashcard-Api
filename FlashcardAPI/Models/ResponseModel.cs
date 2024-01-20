namespace FlashcardAPI.Models
{
    public class ResponseModel
    {
        public bool Status { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; } = null;

    }
}
