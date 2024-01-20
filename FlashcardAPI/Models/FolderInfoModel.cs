namespace FlashcardAPI.Models
{
    public class FolderInfoModel
    {
        public bool Status { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; } = null;
        public List<FolderData> Data { get; set; }
    }
}
