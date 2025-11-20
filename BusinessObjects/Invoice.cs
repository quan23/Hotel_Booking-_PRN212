using System;
using System.Collections.Generic;

namespace BusinessObjects;

public partial class Invoice
{
    public int InvoiceId { get; set; }

    public int? BookingId { get; set; }

    public int? CustomerId { get; set; }

    public DateTime InvoiceDate { get; set; }

    public decimal TotalAmount { get; set; }

    public string PaymentMethod { get; set; } = null!;

    public string PaymentStatus { get; set; } = null!;

    public string? Notes { get; set; }

    public virtual Booking? Booking { get; set; }

    public virtual Customer? Customer { get; set; }
}
