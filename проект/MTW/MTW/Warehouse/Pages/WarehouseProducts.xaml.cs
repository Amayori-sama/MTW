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
            TypeComboBox.ItemsSource = SourceCore.db.types.ToList();
            ModelComboBox.ItemsSource = SourceCore.db.models.ToList();
            ManufacturerComboBox.ItemsSource = SourceCore.db.manufacturers.ToList();
            PlaceComboBox.ItemsSource = SourceCore.db.places.ToList();
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
            TypeComboBox.Text = SourceCore.db.types.ToString();
            ModelComboBox.Text = SourceCore.db.models.ToString();
            ManufacturerComboBox.Text = SourceCore.db.manufacturers.ToString();
            PlaceComboBox.Text = SourceCore.db.places.ToString();
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
            TypeComboBox.Text = SelectedProduct.types.name.ToString();
            ModelComboBox.Text = SelectedProduct.models.name.ToString();
            NameTextBox.Text = SelectedProduct.name;
            DescriptionTextBox.Text = SelectedProduct.description.ToString();
            ManufacturerComboBox.Text = SelectedProduct.manufacturers.name.ToString();
            CostTextBox.Text = SelectedProduct.cost.ToString();
            CountTextBox.Text = SelectedProduct.count.ToString();
            WeightTextBox.Text = SelectedProduct.weight.ToString();
            HeightTextBox.Text = SelectedProduct.height.ToString();
            WidthTextBox.Text = SelectedProduct.width.ToString();
            LengthTextBox.Text = SelectedProduct.length.ToString();
            PlaceComboBox.Text = SelectedProduct.places.name.ToString();
            RecordTextImage.Text = SelectedProduct.image.ToString();
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

        private void AddCommit_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (((Base.types)TypeComboBox.SelectedItem == null) || (TypeComboBox.Text == " ...")) errors.AppendLine("Укажите тип");
            if (((Base.models)ModelComboBox.SelectedItem == null) || (ModelComboBox.Text == " ...")) errors.AppendLine("Укажите модель");
            if (string.IsNullOrEmpty(NameTextBox.Text)) errors.AppendLine("Укажите название");
            if (string.IsNullOrEmpty(DescriptionTextBox.Text)) errors.AppendLine("Укажите описание");
            if (((Base.manufacturers)ManufacturerComboBox.SelectedItem == null) || (ManufacturerComboBox.Text == " ...")) errors.AppendLine("Укажите производителя");
            if (string.IsNullOrEmpty(CostTextBox.Text)) errors.AppendLine("Укажите цену");
            if (string.IsNullOrEmpty(CountTextBox.Text)) errors.AppendLine("Укажите количество");
            if (string.IsNullOrEmpty(WeightTextBox.Text)) errors.AppendLine("Укажите вес");
            if (string.IsNullOrEmpty(HeightTextBox.Text)) errors.AppendLine("Укажите высоту");
            if (string.IsNullOrEmpty(WidthTextBox.Text)) errors.AppendLine("Укажите ширину");
            if (string.IsNullOrEmpty(LengthTextBox.Text)) errors.AppendLine("Укажите длину");
            if (((Base.places)PlaceComboBox.SelectedItem == null) || (PlaceComboBox.Text == " ...")) errors.AppendLine("Укажите место");
            string[] buf = RecordTextImage.Text.Split('.');
            if (buf[buf.Length - 1] != "jpg") errors.AppendLine("Укажите название картинки");

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
                    NewBase.types = (Base.types)TypeComboBox.SelectedItem;
                    NewBase.models = (Base.models)ModelComboBox.SelectedItem;
                    NewBase.name = NameTextBox.Text.Trim();
                    NewBase.description = DescriptionTextBox.Text.Trim();
                    NewBase.manufacturers = (Base.manufacturers)ManufacturerComboBox.SelectedItem;
                    NewBase.cost = decimal.Parse(CostTextBox.Text.Trim());
                    NewBase.count = int.Parse(CountTextBox.Text.Trim());
                    NewBase.weight = float.Parse(WeightTextBox.Text.Trim());
                    NewBase.height = float.Parse(HeightTextBox.Text.Trim());
                    NewBase.width = float.Parse(WidthTextBox.Text.Trim());
                    NewBase.length = float.Parse(LengthTextBox.Text.Trim());
                    NewBase.places = (Base.places)PlaceComboBox.SelectedItem;                    
                    NewBase.image = ActionsWithPictures.ConvertImageToBinary(RecordTextImage.Text);
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
                    EditBase.types = (Base.types)TypeComboBox.SelectedItem;
                    EditBase.models = (Base.models)ModelComboBox.SelectedItem;
                    EditBase.name = NameTextBox.Text.Trim();
                    EditBase.description = DescriptionTextBox.Text.Trim();
                    EditBase.manufacturers = (Base.manufacturers)ManufacturerComboBox.SelectedItem;
                    EditBase.cost = decimal.Parse(CostTextBox.Text.Trim());
                    EditBase.count = int.Parse(CountTextBox.Text.Trim());
                    EditBase.weight = float.Parse(WeightTextBox.Text.Trim());
                    EditBase.height = float.Parse(HeightTextBox.Text.Trim());
                    EditBase.width = float.Parse(WidthTextBox.Text.Trim());
                    EditBase.length = float.Parse(LengthTextBox.Text.Trim());
                    EditBase.places = (Base.places)PlaceComboBox.SelectedItem;
                    EditBase.image = ActionsWithPictures.ConvertImageToBinary(RecordTextImage.Text);
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
