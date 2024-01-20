using FlashcardAPI.Data;
namespace FlashcardAPI.Models
{
    public class SetsResponseModel
    {
        public bool Status { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; } = null;

        public List<Set> setList { get; set; }
    }
}
