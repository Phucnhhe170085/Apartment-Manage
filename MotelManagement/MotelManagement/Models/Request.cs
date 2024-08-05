using System;
using System.Collections.Generic;

namespace MotelManagement.Models;

public partial class Request
{
    public int RequesstId { get; set; }

    public int RequestType { get; set; }

    public string Title { get; set; } = null!;

    public string Desciption { get; set; } = null!;

    public DateOnly CreateAt { get; set; }

    public string ResStatus { get; set; } = null!;

    public int UserID { get; set; }
}
