using System;
using System.Collections.Generic;

namespace CRUD_2312B1.Models;

public partial class Product
{
    public int Id { get; set; }

    public string ProductName { get; set; } = null!;

    public int ProductPrice { get; set; }

    public string ProductDescription { get; set; } = null!;

    public string? ProductImage { get; set; }

    public int? CId { get; set; }

    public virtual Category? CIdNavigation { get; set; }
}
