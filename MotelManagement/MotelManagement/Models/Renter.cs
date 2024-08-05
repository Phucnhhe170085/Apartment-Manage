using System;
using System.Collections.Generic;

namespace MotelManagement.Models;

public partial class Renter
{
    public int RenterId { get; set; }

    public int UserId { get; set; }

    public int? RoomId { get; set; }

    public bool RenterStatus { get; set; }

    public bool RenterHaveRoom { get; set; }
}
