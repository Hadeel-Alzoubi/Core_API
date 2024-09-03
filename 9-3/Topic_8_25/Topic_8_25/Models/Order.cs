using System;
using System.Collections.Generic;

namespace WepAPICore.Models;

public partial class Order
{
    public int OrderId { get; set; }

    public int? UserId { get; set; }

    public string? OrderDate { get; set; }

    public virtual User OrderNavigation { get; set; } = null!;
}
