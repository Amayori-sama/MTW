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
    /// Interaction logic for WarehouseCountrys.xaml
    /// </summary>
    public partial class WarehouseCountrys : Page
    {
        private int DlgMode;
        private Base.countrys SelectedCountrys;
        private ObservableCollection<Base.countrys> Countrys;

        public WarehouseCountrys()
        {
            InitializeComponent();
            DataContext = this;
            DlgLoad(false, "");
            UpdateGrid(null);
        }

        private void LoadingCountrys(object sender, RoutedEventArgs e)
        {
            List<string> Columns = new List<string>();
            for (int i = 0; i < 1; i++)
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
        private void UpdateGrid(Base.countrys country)
        {
            if ((country == null) && (PageGrid.ItemsSource != null))
            {
                country = (Base.countrys)PageGrid.SelectedItem;
            }
            Countrys = new ObservableCollection<Base.countrys>(SourceCore.db.countrys);

            PageGrid.ItemsSource = Countrys;
            PageGrid.ItemsSource = SourceCore.db.countrys.ToList();
            PageGrid.SelectedItem = country;
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
            NameTextBox.Text = SelectedCountrys.name;
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
                SelectedCountrys = (Base.countrys)PageGrid.SelectedItem;
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
                SelectedCountrys = (Base.countrys)PageGrid.SelectedItem;
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
                    Base.countrys DeletingAccessory = (Base.countrys)PageGrid.SelectedItem;
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
                    Base.countrys SelectingAccessory = (Base.countrys)PageGrid.SelectedItem;
                    SourceCore.db.countrys.Remove(DeletingAccessory);
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
                    PageGrid.ItemsSource = SourceCore.db.countrys.Where(q => q.name.ToString().Contains(textbox.Text)).ToList();
                    break;

            }
        }

        private void AddCommit_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (string.IsNullOrEmpty(NameTextBox.Text)) errors.AppendLine("Укажите название");
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            if (DlgMode == 0)
            {
                try
                {
                    var NewBase = new Base.countrys();
                    NewBase.name = NameTextBox.Text.Trim();
                    SourceCore.db.countrys.Add(NewBase);
                    SelectedCountrys = NewBase;
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
                    var EditBase = new Base.countrys();
                    EditBase = SourceCore.db.countrys.First(p => p.id == SelectedCountrys.id);
                    EditBase.name = NameTextBox.Text.Trim();
                }
                catch (Exception)
                {
                    MessageBox.Show("Введены некоректные данные");
                }
            }
            try
            {
                SourceCore.db.SaveChanges();
                UpdateGrid(SelectedCountrys);
                DlgLoad(false, "");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void AddRollback_Click(object sender, RoutedEventArgs e)
        {
            UpdateGrid(SelectedCountrys);
            DlgLoad(false, "");
        }
    }
}
