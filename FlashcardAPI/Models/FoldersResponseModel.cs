using FlashcardAPI.Data;

namespace FlashcardAPI.Models
{
    public class FoldersResponseModel
    {
        public bool Status { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; } = null;

        public List<Folder> Folders { get; set; }
    }
}
