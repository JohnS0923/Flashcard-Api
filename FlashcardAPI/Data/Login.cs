using System;
using System.Collections.Generic;

namespace FlashcardAPI.Data;

public partial class Login
{
    public int LoginId { get; set; }

    public int UserId { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;
}
