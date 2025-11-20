using BusinessObjects;
using DataAccessLayer;
using System.Windows;
using System.Windows.Controls;

namespace FUMiniHotel_ProjectPRN212.Invoice
{
    public partial class InvoiceManagement : Page
    {
        private readonly InvoiceDAO _invoiceDao = new InvoiceDAO();

        public InvoiceManagement()
        {
            InitializeComponent();
            LoadInvoices();
        }

        private void LoadInvoices()
        {
            dgInvoices.ItemsSource = _invoiceDao.GetAllInvoices();
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtSearch.Text))
            {
                dgInvoices.ItemsSource = _invoiceDao.SearchInvoicesByCustomer(txtSearch.Text);
            }
            else
            {
                LoadInvoices();
            }
        }

        private void BtnView_Click(object sender, RoutedEventArgs e)
        {
            if (dgInvoices.SelectedItem is BusinessObjects.Invoice selectedInvoice)
            {
                var detailWindow = new InvoiceDetailWindow(selectedInvoice);
                detailWindow.ShowDialog();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một hóa đơn để xem", "Thông báo",
                              MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void BtnPrint_Click(object sender, RoutedEventArgs e)
        {
            if (dgInvoices.SelectedItem is BusinessObjects.Invoice selectedInvoice)
            {
                if (MessageBox.Show("Bạn có chắc chắn muốn in hóa đơn này?",
                    "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    // Print logic here
                    MessageBox.Show("In hóa đơn thành công!", "Thông báo",
                                  MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một hóa đơn để in", "Thông báo",
                              MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void BtnFilter_Click(object sender, RoutedEventArgs e)
        {
            //var filterDialog = new InvoiceFilterDialog();
            //if (filterDialog.ShowDialog() == true)
            //{
            //    dgInvoices.ItemsSource = _invoiceDao.GetFilteredInvoices(
            //        filterDialog.FromDate,
            //        filterDialog.ToDate,
            //        filterDialog.PaymentStatus);
            //}
        }
    }
}