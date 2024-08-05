using System;
using System.Collections.Generic;

namespace MotelManagement.Models;

public partial class RoomHistory
{
    public int HistoryId { get; set; }

    public DateOnly CreateAt { get; set; }

    public int RenterId { get; set; }

    public int RoomId { get; set; }

    public DateOnly? CheckIn { get; set; }

    public DateOnly? CheckOut { get; set; }

    public int? UserId { get; set; }

    public bool Status { get; set; }
}
