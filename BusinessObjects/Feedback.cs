using System;
using System.Collections.Generic;

namespace BusinessObjects;

public partial class Feedback
{
    public int FeedbackId { get; set; }

    public int? CustomerId { get; set; }

    public int? BookingId { get; set; }

    public DateTime FeedbackDate { get; set; }

    public int Rating { get; set; }

    public string? Comment { get; set; }

    public virtual Booking? Booking { get; set; }

    public virtual Customer? Customer { get; set; }
}
