using System;
using System.Collections.Generic;

namespace FlashcardAPI.Data;

public partial class Card
{
    public int CardId { get; set; }

    public int SetId { get; set; }

    public string CardFront { get; set; } = null!;

    public string CardBack { get; set; } = null!;

    public bool? Starred { get; set; }
}
