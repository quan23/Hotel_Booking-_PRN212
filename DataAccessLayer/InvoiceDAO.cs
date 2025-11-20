using BusinessObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccessLayer
{
    public class InvoiceDAO
    {
        // Get all invoices with customer and booking info
        public List<Invoice> GetAllInvoices()
        {
            using (var context = new FuminiHotelProjectPrn212Context())
            {
                return context.Invoices
                    .Include(i => i.Customer)
                    .Include(i => i.Booking)
                        .ThenInclude(b => b.BookingDetails)
                            .ThenInclude(bd => bd.Room)
                                .ThenInclude(r => r.RoomType)
                    .Include(i => i.Booking)
                        .ThenInclude(b => b.BookingServices)
                            .ThenInclude(bs => bs.Service)
                    .OrderByDescending(i => i.InvoiceDate)
                    .ToList();
            }
        }

        // Search invoices by customer name
        public List<Invoice> SearchInvoicesByCustomer(string customerName)
        {
            using (var context = new FuminiHotelProjectPrn212Context())
            {
                return context.Invoices
                    .Include(i => i.Customer)
                    .Include(i => i.Booking)
                        .ThenInclude(b => b.BookingDetails)
                            .ThenInclude(bd => bd.Room)
                                .ThenInclude(r => r.RoomType)
                    .Include(i => i.Booking)
                        .ThenInclude(b => b.BookingServices)
                            .ThenInclude(bs => bs.Service)
                    .Where(i => i.Customer.FullName.Contains(customerName))
                    .OrderByDescending(i => i.InvoiceDate)
                    .ToList();
            }
        }

        // Get filtered invoices by date range and payment status
        public List<Invoice> GetFilteredInvoices(DateTime? fromDate, DateTime? toDate, string paymentStatus)
        {
            using (var context = new FuminiHotelProjectPrn212Context())
            {
                var query = context.Invoices
                    .Include(i => i.Customer)
                    .Include(i => i.Booking)
                        .ThenInclude(b => b.BookingDetails)
                            .ThenInclude(bd => bd.Room)
                                .ThenInclude(r => r.RoomType)
                    .Include(i => i.Booking)
                        .ThenInclude(b => b.BookingServices)
                            .ThenInclude(bs => bs.Service)
                    .AsQueryable();

                if (fromDate != null)
                {
                    query = query.Where(i => i.InvoiceDate >= fromDate);
                }

                if (toDate != null)
                {
                    query = query.Where(i => i.InvoiceDate <= toDate);
                }

                if (!string.IsNullOrEmpty(paymentStatus))
                {
                    query = query.Where(i => i.PaymentStatus == paymentStatus);
                }

                return query.OrderByDescending(i => i.InvoiceDate).ToList();
            }
        }

        // Get invoice by ID with full details
        public Invoice GetInvoiceById(int invoiceId)
        {
            using (var context = new FuminiHotelProjectPrn212Context())
            {
                return context.Invoices
                    .Include(i => i.Customer)
                    .Include(i => i.Booking)
                    .ThenInclude(b => b.BookingDetails)
                    .ThenInclude(bd => bd.Room)
                    .ThenInclude(r => r.RoomType)
                    .Include(i => i.Booking)
                    .ThenInclude(b => b.BookingServices)
                    .ThenInclude(bs => bs.Service)
                    .FirstOrDefault(i => i.InvoiceId == invoiceId);
            }
        }

        // Generate invoice from booking
        public Invoice GenerateInvoiceFromBooking(int bookingId, string paymentMethod, string notes = null)
        {
            using (var context = new FuminiHotelProjectPrn212Context())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var booking = context.Bookings
                            .Include(b => b.Customer)
                            .Include(b => b.BookingDetails)
                            .Include(b => b.BookingServices)
                            .FirstOrDefault(b => b.BookingId == bookingId);

                        if (booking == null) return null;

                        var invoice = new Invoice
                        {
                            BookingId = bookingId,
                            CustomerId = booking.CustomerId,
                            InvoiceDate = DateTime.Now,
                            TotalAmount = booking.TotalPrice,
                            PaymentMethod = paymentMethod,
                            PaymentStatus = "Paid", // Default to paid when generated
                            Notes = notes
                        };

                        context.Invoices.Add(invoice);
                        context.SaveChanges();

                        // Update booking status if needed
                        booking.Status = "Completed";
                        context.Bookings.Update(booking);
                        context.SaveChanges();

                        transaction.Commit();
                        return invoice;
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        // Update payment status
        public bool UpdatePaymentStatus(int invoiceId, string newStatus)
        {
            using (var context = new FuminiHotelProjectPrn212Context())
            {
                var invoice = context.Invoices.Find(invoiceId);
                if (invoice != null)
                {
                    invoice.PaymentStatus = newStatus;
                    context.SaveChanges();
                    return true;
                }
                return false;
            }
        }

        // Delete invoice
        public bool DeleteInvoice(int invoiceId)
        {
            using (var context = new FuminiHotelProjectPrn212Context())
            {
                var invoice = context.Invoices.Find(invoiceId);
                if (invoice != null)
                {
                    context.Invoices.Remove(invoice);
                    context.SaveChanges();
                    return true;
                }
                return false;
            }
        }
    }
}