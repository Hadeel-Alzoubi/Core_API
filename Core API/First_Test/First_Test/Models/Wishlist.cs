using System;
using System.Collections.Generic;

namespace First_Test.Models;

public partial class Wishlist
{
    public int WishlistId { get; set; }

    public int? UserId { get; set; }

    public int? ProductId { get; set; }
}
