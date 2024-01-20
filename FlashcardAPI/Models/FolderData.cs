namespace FlashcardAPI.Models
{
    public class FolderData
    {
        public int FolderId { get; set; }

        public int UserId { get; set; }

        public string FolderTitle { get; set; } = null!;

        public string FolderDescription { get; set; } = null!;
        public string UserName { get; set; }
        public int SetCount { get; set; }
    }
}
