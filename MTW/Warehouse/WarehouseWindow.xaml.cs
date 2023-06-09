using System.Windows;
using System.Windows.Controls;

namespace MTW.Warehouse
{
    /// <summary>
    /// Interaction logic for WarehouseWindow.xaml
    /// </summary>
    public partial class WarehouseWindow : Window
    {

        public WarehouseWindow()
        {
            InitializeComponent();

        }
        private void BookButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(new Pages.WarehouseProducts());
        }
    }
}
