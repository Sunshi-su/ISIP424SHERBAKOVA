using System;
using System.Collections.Generic;

namespace Praktica.Entities;

public partial class Product
{
    public int Id { get; set; }

    public string Name { get; set; }

    public int Price { get; set; }

    public string Description { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual ICollection<ShoppingCardProduct> ShoppingCardProducts { get; set; } = new List<ShoppingCardProduct>();
}
