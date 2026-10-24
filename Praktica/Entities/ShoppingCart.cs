using System;
using System.Collections.Generic;

namespace Praktica.Entities;

public partial class ShoppingCart
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public virtual ICollection<ShoppingCardProduct> ShoppingCardProducts { get; set; } = new List<ShoppingCardProduct>();

    public virtual User User { get; set; }
}
