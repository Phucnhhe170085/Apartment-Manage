using System;
using System.Collections.Generic;

namespace MotelManagement.Models;

public partial class User
{
    public int UserId { get; set; }

    public string UserName { get; set; } = null!;

    public string UserGender { get; set; } = null!;

    public DateOnly UserBirth { get; set; }

    public string UserAddress { get; set; } = null!;

    public string UserPhone { get; set; } = null!;

    public virtual Account UserNavigation { get; set; } = null!;
}
