using DataAccessLayer;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace FUMiniHotel_ProjectPRN212.Booking
{
    public partial class BookingManagement : Page
    {
        private readonly BookingDAO _bookingDao = new BookingDAO();
        private List<BusinessObjects.Booking> _allBookings;
        private readonly CustomerService _customerService;

        public BookingManagement()
        {
            _customerService = new CustomerService();
            InitializeComponent();
            LoadAllBookings();
        }

        private void LoadAllBookings()
        {
            _allBookings = _bookingDao.GetAllBookingsWithDetails();
            dgBookings.ItemsSource = _allBookings; // Bind the data to DataGrid
        }

        private void ViewDetails_Click(object sender, RoutedEventArgs e)
        {
            var selectedBooking = dgBookings.SelectedItem as BusinessObjects.Booking;
            if (selectedBooking != null)
            {
                var detailsWindow = new BookingDetailsWindow(selectedBooking);
                detailsWindow.ShowDialog();
            }
            else
            {
                MessageBox.Show("Please select a booking to view details.", "Information",
                              MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (_allBookings == null) return;

            var searchText = txtSearch.Text.ToLower();

            if (string.IsNullOrEmpty(searchText))
            {
                dgBookings.ItemsSource = _allBookings;
            }
            else
            {
                // Filter bookings where customer name contains the search text
                var filteredBookings = _allBookings
                    .Where(b => b.Customer != null &&
                               b.Customer.FullName.ToLower().Contains(searchText))
                    .ToList();

                dgBookings.ItemsSource = filteredBookings;
            }
        }
    }
}