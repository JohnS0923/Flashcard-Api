using System;
using System.Collections.Generic;

namespace FlashcardAPI.Data;

public partial class SetFolder
{
    public int SetFolderId { get; set; }

    public int SetId { get; set; }

    public int FolderId { get; set; }
}
