using FlashcardAPI.Data;
namespace FlashcardAPI.Models
{
    public class CardsResponseModel
    {
        public bool Status { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; } = null;
        public string SetName { get; set; }

        public List<Card> cardsList { get; set; }


    }
}
