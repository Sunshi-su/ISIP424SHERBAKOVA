using System;
using System.Collections.Generic;

namespace Praktica.Entities;

public partial class Order
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public DateOnly OrderDate { get; set; }

    public bool IsReceived { get; set; }

    public int OrderPickupPointId { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual OrderPickupPoint OrderPickupPoint { get; set; }

    public virtual User User { get; set; }
}
