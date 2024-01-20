using System;
using System.Collections.Generic;

namespace FlashcardAPI.Data;

public partial class Folder
{
    public int FolderId { get; set; }

    public int UserId { get; set; }

    public string FolderTitle { get; set; } = null!;

    public string FolderDescription { get; set; } = null!;
}
