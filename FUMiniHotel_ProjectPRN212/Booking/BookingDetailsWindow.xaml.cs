using BusinessObjects;
using DataAccessLayer;
using System.Windows;

namespace FUMiniHotel_ProjectPRN212.Booking
{
    public partial class BookingDetailsWindow : Window
    {
        private readonly BookingDAO _bookingDao = new BookingDAO();
        private readonly BusinessObjects.Booking _booking;

        public BookingDetailsWindow(BusinessObjects.Booking booking)
        {
            InitializeComponent();
            _booking = booking;
            LoadBookingData();
        }

        private void LoadBookingData()
        {
            try
            {
                // Set the main booking info as DataContext
                this.DataContext = _booking;

                // Load and display room booking details
                var roomDetails = _bookingDao.GetBookingDetails(_booking.BookingId);
                dgBookingDetails.ItemsSource = roomDetails;

                // Load and display service booking details
                var serviceDetails = _bookingDao.GetBookingServices(_booking.BookingId);
                dgBookingServices.ItemsSource = serviceDetails;

                // Calculate and display total price (if not already bound)
                // This is optional since we're already binding to TotalPrice
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu đặt phòng: {ex.Message}",
                              "Lỗi",
                              MessageBoxButton.OK,
                              MessageBoxImage.Error);
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}