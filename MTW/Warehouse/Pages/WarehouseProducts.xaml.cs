using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace MTW.Warehouse.Pages
{
    /// <summary>
    /// Interaction logic for WarehouseProducts.xaml
    /// </summary>
    public partial class WarehouseProducts : Page
    {
        private int DlgMode;
        private Base.products SelectedProduct;
        private ObservableCollection<Base.products> Products;
        public WarehouseProducts()
        {
            InitializeComponent();
            DataContext = this;
            DlgLoad(false, "");
            ManufacturerComboBox.ItemsSource = SourceCore.db.manufacturers.ToList();
            ModelComboBox.ItemsSource = SourceCore.db.models.ToList();
        }

        private void LoadingProd(object sender, RoutedEventArgs e)
        {
            List<string> Columns = new List<string>();
            for (int i = 0; i < 4; i++)
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
            ManufacturerComboBox.Text = SourceCore.db.manufacturers.ToString();
            ModelComboBox.Text = SourceCore.db.models.ToString();
        }

        private void DlgLoad(bool b, string DlgModeContent)
        {
            if (b == true)
            {
                ColumnChange.Width = new GridLength(300);
                PageGrid.IsHitTestVisible = false;
                RecordLabel.Content = DlgModeContent + " запись";
                AddCommit.Content = DlgModeContent;
                RecordAdd.IsEnabled = false;
                RecordCopy.IsEnabled = false;
                RecordEdit.IsEnabled = false;
                RecordDellete.IsEnabled = false;
            }
            else
            {
                ColumnChange.Width = new GridLength(0);
                PageGrid.IsHitTestVisible = true;
                RecordAdd.IsEnabled = true;
                RecordCopy.IsEnabled = true;
                RecordEdit.IsEnabled = true;
                RecordDellete.IsEnabled = true;
                DlgMode = -1;
            }
        }

        private void FillTextBox()
        {
            RecordTextBookName.Text = SelectedProduct.name;
            ManufacturerComboBox.Text = SelectedProduct.manufacturers.name.ToString();
            ModelComboBox.Text = SelectedProduct.models.name.ToString();

        }

        private void RecordAdd_Click(object sender, RoutedEventArgs e)
        {
            DlgLoad(true, "Добавить");
            DataContext = null;
            DlgMode = 0;
        }

        private void RecordkCopy_Click(object sender, RoutedEventArgs e)
        {
            if (PageGrid.SelectedItem != null)
            {
                DlgLoad(true, "Копировать");
                SelectedProduct = (Base.products)PageGrid.SelectedItem;
                FillTextBox();
                DlgMode = 0;
            }
            else
            {
                MessageBox.Show("Не выбрано ни одной строки!", "Сообщение", MessageBoxButton.OK);
            }
        }

        private void RecordEdit_Click(object sender, RoutedEventArgs e)
        {
            if (PageGrid.SelectedItem != null)
            {
                DlgLoad(true, "Изменить");
                SelectedProduct = (Base.products)PageGrid.SelectedItem;
                FillTextBox();
            }
            else
            {
                MessageBox.Show("Не выбрано ни одной строки!", "Сообщение", MessageBoxButton.OK);
            }
        }

        private void RecordDelete_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Удалить запись?", "Внимание", MessageBoxButton.OKCancel, MessageBoxImage.Warning) == MessageBoxResult.OK)
            {

                try
                {
                    // Ссылка на удаляемую книгу
                    Base.products DeletingAccessory = (Base.products)PageGrid.SelectedItem;
                    // Определение ссылки, на которую должен перейти указатель после удаления
                    if (PageGrid.SelectedIndex < PageGrid.Items.Count - 1)
                    {
                        PageGrid.SelectedIndex++;
                    }
                    else
                    {
                        if (PageGrid.SelectedIndex > 0)
                        {
                            PageGrid.SelectedIndex--;
                        }
                    }
                    Base.products SelectingAccessory = (Base.products)PageGrid.SelectedItem;
                    SourceCore.db.products.Remove(DeletingAccessory);
                    SourceCore.db.SaveChanges();
                    UpdateGrid(SelectingAccessory);
                }
                catch
                {

                    MessageBox.Show("Невозможно удалить запись, так как она используется в других справочниках базы данных.",
                    "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.None);
                }
            }
        }

        private void FilterTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textbox = sender as TextBox;
            switch (FilterComboBox.SelectedIndex)
            {
                case 0:
                    PageGrid.ItemsSource = SourceCore.db.products.Where(q => q.name.Contains(textbox.Text)).ToList();
                    break;
                case 1:
                    PageGrid.ItemsSource = SourceCore.db.products.Where(q => q.models.name.ToString().Contains(textbox.Text)).ToList();
                    break;
                case 2:
                    PageGrid.ItemsSource = SourceCore.db.products.Where(q => q.manufacturers.name.ToString().Contains(textbox.Text)).ToList();
                    break;
                case 3:
                    PageGrid.ItemsSource = SourceCore.db.products.Where(q => q.description.ToString().Contains(textbox.Text)).ToList();
                    break;
            }
        }

        private void AddCommit_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrEmpty(RecordTextBookName.Text))
                errors.AppendLine("Укажите название книги");

            if (((Base.manufacturers)ManufacturerComboBox.SelectedItem == null) || (ManufacturerComboBox.Text == " ..."))
                errors.AppendLine("Укажите автора");

            if (((Base.models)ModelComboBox.SelectedItem == null) || (ModelComboBox.Text == " ..."))
                errors.AppendLine("Укажите издательство");

            if (string.IsNullOrEmpty(RecordTextGenres.Text))
                errors.AppendLine("Укажите описание");

            //if (string.IsNullOrEmpty(RecordTextImage.Text))
                //errors.AppendLine("Укажите название картинки");

            //string[] buf = RecordTextImage.Text.Split('.');
            //if (buf[buf.Length - 1] != "jpg")
                //errors.AppendLine("Укажите название картинки");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            if (DlgMode == 0)
            {
                try
                {
                    var NewBase = new Base.products();
                    NewBase.name = RecordTextBookName.Text.Trim();
                    NewBase.models = (Base.models)ModelComboBox.SelectedItem;
                    NewBase.manufacturers = (Base.manufacturers)ManufacturerComboBox.SelectedItem;
                    NewBase.description = RecordTextGenres.Text.Trim();
                    //NewBase.image = ActionsWithPictures.ConvertImageToBinary(RecordTextImage.Text);
                    SourceCore.db.products.Add(NewBase);
                    SelectedProduct = NewBase;
                }
                catch (Exception)
                {
                    MessageBox.Show("Введены некоректные данные");
                }
            }
            else
            {
                try
                {
                    var EditBase = new Base.products();
                    EditBase = SourceCore.db.products.First(p => p.id == SelectedProduct.id);
                    EditBase.name = RecordTextBookName.Text.Trim();
                    EditBase.models = (Base.models)ModelComboBox.SelectedItem;
                    EditBase.manufacturers = (Base.manufacturers)ManufacturerComboBox.SelectedItem;
                    EditBase.description = RecordTextGenres.Text.Trim();
                    //EditBase.image = ActionsWithPictures.ConvertImageToBinary(RecordTextImage.Text);
                }
                catch (Exception)
                {
                    MessageBox.Show("Введены некоректные данные");
                }
            }

            try
            {
                SourceCore.db.SaveChanges();
                UpdateGrid(SelectedProduct);
                DlgLoad(false, "");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void AddRollback_Click(object sender, RoutedEventArgs e)
        {
            UpdateGrid(SelectedProduct);
            DlgLoad(false, "");
        }

        private void SelectFileButton_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                RecordTextImage.Text = openFileDialog.FileName;
            }
        }
    }
}
