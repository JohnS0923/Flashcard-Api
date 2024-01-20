using FlashcardAPI.Data;

namespace FlashcardAPI.Models
{
    public class SetInfoResponseModel
    {
        public bool Status { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; } = null;
        
        public Set setInfo { get; set; }
    }
}
