using System;
using System.Collections.Generic;

namespace Praktica.Entities;

public partial class OrderPickupPoint
{
    public int Id { get; set; }

    public string Address { get; set; }

    public TimeOnly OpeningTime { get; set; }

    public TimeOnly ClosingTime { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
