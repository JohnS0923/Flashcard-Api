using FlashcardAPI.Data;

namespace FlashcardAPI.Models
{
    public class CardResponseModel
    {
        public bool Status { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; } = null;

        public Card Card { get; set; }
    }
}
