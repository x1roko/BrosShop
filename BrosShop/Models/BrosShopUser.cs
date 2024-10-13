using System;
using System.Collections.Generic;

namespace BrosShop.Models;

public partial class BrosShopUser
{
    public int BrosShopUserId { get; set; }

    public string BrosShopUsername { get; set; } = null!;

    public string BrosShopPassword { get; set; } = null!;

    public string BrosShopEmail { get; set; } = null!;

    public string? BrosShopFullName { get; set; }

    public DateTime BrosShopRegistrationDate { get; set; }

    public virtual ICollection<BrosShopReview> BrosShopReviews { get; set; } = new List<BrosShopReview>();
}
