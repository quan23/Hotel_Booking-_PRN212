using System;
using System.Collections.Generic;

namespace BusinessObjects;

public partial class BookingDetail
{
    public int BookingDetailId { get; set; }

    public int? BookingId { get; set; }
    public int? RoomId { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public decimal ActualPrice { get; set; }

    public virtual Booking? Booking { get; set; }

    public virtual Room? Room { get; set; }
}
