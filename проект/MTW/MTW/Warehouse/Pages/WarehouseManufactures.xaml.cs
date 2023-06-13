using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace MTW.Warehouse.Pages
{
    /// <summary>
    /// Interaction logic for WarehouseManufactures.xaml
    /// </summary>
    public partial class WarehouseManufactures : Page
    {
        private int DlgMode;
        private Base.manufacturers SelectedManufactures;
        private ObservableCollection<Base.manufacturers> Manufacturers;

        public WarehouseManufactures()
        {
            InitializeComponent();
            DataContext = this;
            DlgLoad(false, ""); 
            CountryComboBox.ItemsSource = SourceCore.db.countrys.ToList();
            UpdateGrid(null);
        }

        private void LoadingManufactures(object sender, RoutedEventArgs e)
        {
            List<string> Columns = new List<string>();
            for (int i = 0; i < 2; i++)
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
        private void UpdateGrid(Base.manufacturers manufacturer)
        {
            if ((manufacturer == null) && (PageGrid.ItemsSource != null))
            {
                manufacturer = (Base.manufacturers)PageGrid.SelectedItem;
            }
            Manufacturers = new ObservableCollection<Base.manufacturers>(SourceCore.db.manufacturers);

            PageGrid.ItemsSource = Manufacturers;
            PageGrid.ItemsSource = SourceCore.db.manufacturers.ToList();
            CountryComboBox.ItemsSource = SourceCore.db.countrys.ToString();
            PageGrid.SelectedItem = manufacturer;
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
            NameTextBox.Text = SelectedManufactures.name;
            CountryComboBox.Text = SelectedManufactures.countrys.name.ToString();
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
                SelectedManufactures = (Base.manufacturers)PageGrid.SelectedItem;
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
                SelectedManufactures = (Base.manufacturers)PageGrid.SelectedItem;
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
                    Base.manufacturers DeletingAccessory = (Base.manufacturers)PageGrid.SelectedItem;
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
                    Base.manufacturers SelectingAccessory = (Base.manufacturers)PageGrid.SelectedItem;
                    SourceCore.db.manufacturers.Remove(DeletingAccessory);
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
                    PageGrid.ItemsSource = SourceCore.db.manufacturers.Where(q => q.name.ToString().Contains(textbox.Text)).ToList();
                    break;
                case 1:
                    PageGrid.ItemsSource = SourceCore.db.manufacturers.Where(q => q.countrys.name.ToString().Contains(textbox.Text)).ToList();
                    break;
            }
        }

        private void AddCommit_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (string.IsNullOrEmpty(NameTextBox.Text)) errors.AppendLine("Укажите название");
            if (((Base.countrys)CountryComboBox.SelectedItem == null) || (CountryComboBox.Text == " ...")) errors.AppendLine("Укажите страну");
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            if (DlgMode == 0)
            {
                try
                {
                    var NewBase = new Base.manufacturers();
                    NewBase.name = NameTextBox.Text.Trim();
                    NewBase.countrys = (Base.countrys)CountryComboBox.SelectedItem;
                    SourceCore.db.manufacturers.Add(NewBase);
                    SelectedManufactures = NewBase;
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
                    var EditBase = new Base.manufacturers();
                    EditBase = SourceCore.db.manufacturers.First(p => p.id == SelectedManufactures.id);
                    EditBase.name = NameTextBox.Text.Trim();
                    EditBase.countrys = (Base.countrys)CountryComboBox.SelectedItem;
                }
                catch (Exception)
                {
                    MessageBox.Show("Введены некоректные данные");
                }
            }
            try
            {
                SourceCore.db.SaveChanges();
                UpdateGrid(SelectedManufactures);
                DlgLoad(false, "");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void AddRollback_Click(object sender, RoutedEventArgs e)
        {
            UpdateGrid(SelectedManufactures);
            DlgLoad(false, "");
        }
    }
}
