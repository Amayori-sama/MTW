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
            Frame.Navigate(new Pages.WarehouseProducts());

        }
        private void ProductsBtn(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(new Pages.WarehouseProducts());
        }
        private void ManufacturesBtn(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(new Pages.WarehouseManufactures());
        }
        private void CountrysBtn(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(new Pages.WarehouseCountrys());
        }
        private void ModelsBtn(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(new Pages.WarehouseModels());
        }
        private void TypesBtn(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(new Pages.WarehouseTypes());
        }
        private void UsersBtn(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(new Pages.WarehouseUsers());
        }
    }
}
