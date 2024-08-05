using System;
using System.Collections.Generic;

namespace MotelManagement.Models;

public partial class RoomItem
{
    public int RoomItemId { get; set; }

    public int RoomId { get; set; }

    public int ItemId { get; set; }

    public int Quantity { get; set; }

    public virtual Item Item { get; set; } = null!;

    public virtual Room Room { get; set; } = null!;
}
