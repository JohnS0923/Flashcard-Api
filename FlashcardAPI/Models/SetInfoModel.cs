namespace FlashcardAPI.Models
{
    public class SetInfoModel
    {
        public bool Status { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; } = null;
        public List<SetData> Data { get; set; }
        public string FolderTitle { get; set; }
    }
}
