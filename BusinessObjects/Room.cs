using System;
using System.Collections.Generic;

namespace BusinessObjects;

public partial class Room
{
    public int RoomId { get; set; }

    public string RoomNumber { get; set; } = null!;

    public string? Description { get; set; }

    public int MaxCapacity { get; set; }

    public int? RoomTypeId { get; set; }

    public string Status { get; set; } = null!;

    public decimal PricePerDay { get; set; }

    public virtual ICollection<BookingDetail> BookingDetails { get; set; } = new List<BookingDetail>();

    public virtual RoomType? RoomType { get; set; }
}
