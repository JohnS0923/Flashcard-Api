using FlashcardAPI.Data;

namespace FlashcardAPI.Models
{
    public class SetCreationModel
    {
        public int UserID { get; set; }
        public string SetTitle { get; set; }
        public string SetDescription { get; set; }
        public List<Card> CardList { get; set; }
    }
}
