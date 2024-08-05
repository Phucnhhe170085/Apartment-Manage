using System;
using System.Collections.Generic;

namespace MotelManagement.Models;

public partial class Item
{
    public int ItemId { get; set; }

    public string ItemName { get; set; } = null!;

    public virtual ICollection<RoomItem> RoomItems { get; set; } = new List<RoomItem>();
}
