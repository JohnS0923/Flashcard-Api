namespace FlashcardAPI.Models
{
    public class SetData
    {
        public int SetId { get; set; }

        public int  UserId  { get; set; }

        public string UserName { get; set; }

        public string SetTitle { get; set; } = null!;

        public string SetDescription { get; set; } = null!;
        public int CardCount { get; set; }
        public List<int> FolderConnection {  get; set; }

    }
}
