using System;
using System.Collections.Generic;

namespace MotelManagement.Models;

public partial class Account
{
    public int UserId { get; set; }

    public string UserMail { get; set; } = null!;

    public string UserPassword { get; set; } = null!;

    public int UserRole { get; set; }

    public virtual User? User { get; set; }
}
