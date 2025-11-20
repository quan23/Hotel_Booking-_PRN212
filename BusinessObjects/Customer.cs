using System;
using System.Collections.Generic;

namespace BusinessObjects;

public partial class Customer
{
    public int CustomerId { get; set; }

    public int? UserId { get; set; }

    public string FullName { get; set; } = null!;

    public string? Telephone { get; set; }

    public string? Email { get; set; }

    public DateTime? Birthday { get; set; }

    public string Status { get; set; } = null!;

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();

    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();

    public virtual User? User { get; set; }
}
