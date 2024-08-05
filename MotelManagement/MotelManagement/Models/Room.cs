using System;
using System.Collections.Generic;

namespace MotelManagement.Models;

public partial class Room
{
    public int RoomId { get; set; }

    public int RoomFloor { get; set; }

    public int RoomNumber { get; set; }

    public int RoomSize { get; set; }

    public decimal RoomFee { get; set; }

    public int? RoomStatus { get; set; }

    public virtual ICollection<RoomHistory> RoomHistories { get; set; } = new List<RoomHistory>();
    public virtual ICollection<RoomItem> RoomItems { get; set; } = new List<RoomItem>();
}
