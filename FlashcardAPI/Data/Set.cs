using System;
using System.Collections.Generic;

namespace FlashcardAPI.Data;

public partial class Set
{
    public int SetId { get; set; }

    public int UserId { get; set; }

    public string SetTitle { get; set; } = null!;

    public string SetDescription { get; set; } = null!;
}
