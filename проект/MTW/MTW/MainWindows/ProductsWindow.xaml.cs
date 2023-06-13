using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace MTW.MainWindows
{
    /// <summary>
    /// Interaction logic for ProductsWindow.xaml
    /// </summary>
    public partial class ProductsWindow : Window
    {
        private int DlgMode;
        private Base.products SelectedProduct;
        private ObservableCollection<Base.products> Products;

        public ProductsWindow()
        {
            InitializeComponent();
            DataContext = this;
            DlgLoad(false, "");
            UpdateGrid(null);
        }

        private void LoadingProd(object sender, RoutedEventArgs e)
        {
            List<string> Columns = new List<string>();
            for (int i = 0; i < 12; i++)
            {
                Columns.Add(PageGrid.Columns[i].Header.ToString());
            }
            FilterComboBox.ItemsSource = Columns;
            FilterComboBox.SelectedIndex = 0;

            foreach (DataGridColumn Column in PageGrid.Columns)
            {
                Column.CanUserSort = false;
            }
        }
        private void UpdateGrid(Base.products product)
        {
            if ((product == null) && (PageGrid.ItemsSource != null))
            {
                product = (Base.products)PageGrid.SelectedItem;
            }
            Products = new ObservableCollection<Base.products>(SourceCore.db.products);

            PageGrid.ItemsSource = Products;
            PageGrid.ItemsSource = SourceCore.db.products.ToList();
            PageGrid.SelectedItem = product;
        }

        private void DlgLoad(bool b, string DlgModeContent)
        {
            if (b == true)
            {
                ColumnChange.Width = new GridLength(300);
                PageGrid.IsHitTestVisible = false;
            }
            else
            {
                ColumnChange.Width = new GridLength(0);
                PageGrid.IsHitTestVisible = true;
                DlgMode = -1;
            }
        }

        private void FilterTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textbox = sender as TextBox;
            switch (FilterComboBox.SelectedIndex)
            {
                case 0:
                    PageGrid.ItemsSource = SourceCore.db.products.Where(q => q.types.name.ToString().Contains(textbox.Text)).ToList();
                    break;
                case 1:
                    PageGrid.ItemsSource = SourceCore.db.products.Where(q => q.models.name.ToString().Contains(textbox.Text)).ToList();
                    break;
                case 2:
                    PageGrid.ItemsSource = SourceCore.db.products.Where(q => q.name.Contains(textbox.Text)).ToList();
                    break;
                case 3:
                    PageGrid.ItemsSource = SourceCore.db.products.Where(q => q.description.Contains(textbox.Text)).ToList();
                    break;
                case 4:
                    PageGrid.ItemsSource = SourceCore.db.products.Where(q => q.manufacturers.name.ToString().Contains(textbox.Text)).ToList();
                    break;
                case 5:
                    PageGrid.ItemsSource = SourceCore.db.products.Where(q => q.cost.ToString().Contains(textbox.Text)).ToList();
                    break;
                case 6:
                    PageGrid.ItemsSource = SourceCore.db.products.Where(q => q.count.ToString().Contains(textbox.Text)).ToList();
                    break;
                case 7:
                    PageGrid.ItemsSource = SourceCore.db.products.Where(q => q.weight.ToString().Contains(textbox.Text)).ToList();
                    break;
                case 8:
                    PageGrid.ItemsSource = SourceCore.db.products.Where(q => q.height.ToString().Contains(textbox.Text)).ToList();
                    break;
                case 9:
                    PageGrid.ItemsSource = SourceCore.db.products.Where(q => q.width.ToString().Contains(textbox.Text)).ToList();
                    break;
                case 10:
                    PageGrid.ItemsSource = SourceCore.db.products.Where(q => q.length.ToString().Contains(textbox.Text)).ToList();
                    break;
                case 11:
                    PageGrid.ItemsSource = SourceCore.db.products.Where(q => q.places.name.ToString().Contains(textbox.Text)).ToList();
                    break;
            }
        }
    }
}
