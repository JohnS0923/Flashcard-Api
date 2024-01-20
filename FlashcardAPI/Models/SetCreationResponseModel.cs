using FlashcardAPI.Data;

namespace FlashcardAPI.Models
{
    public class SetCreationResponseModel
    {
        public bool Status { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; } = null;
        public int UserID { get; set; }
        public string SetTitle { get; set; }
        public string SetDescription { get; set; }
        public List<Card> CardList { get; set; }
    }
}
