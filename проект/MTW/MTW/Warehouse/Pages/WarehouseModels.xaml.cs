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
    /// Interaction logic for WarehouseModels.xaml
    /// </summary>
    public partial class WarehouseModels : Page
    {
        private int DlgMode;
        private Base.models SelectedModels;
        private ObservableCollection<Base.models> Models;

        public WarehouseModels()
        {
            InitializeComponent();
            DataContext = this;
            DlgLoad(false, "");
            UpdateGrid(null);
        }

        private void LoadingModels(object sender, RoutedEventArgs e)
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
        private void UpdateGrid(Base.models model)
        {
            if ((model == null) && (PageGrid.ItemsSource != null))
            {
                model = (Base.models)PageGrid.SelectedItem;
            }
            Models = new ObservableCollection<Base.models>(SourceCore.db.models);

            PageGrid.ItemsSource = Models;
            PageGrid.ItemsSource = SourceCore.db.models.ToList();
            PageGrid.SelectedItem = model;
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
            NameTextBox.Text = SelectedModels.name;
            YearTextBox.Text = SelectedModels.year.ToString();
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
                SelectedModels = (Base.models)PageGrid.SelectedItem;
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
                SelectedModels = (Base.models)PageGrid.SelectedItem;
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
                    Base.models DeletingAccessory = (Base.models)PageGrid.SelectedItem;
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
                    Base.models SelectingAccessory = (Base.models)PageGrid.SelectedItem;
                    SourceCore.db.models.Remove(DeletingAccessory);
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
                    PageGrid.ItemsSource = SourceCore.db.models.Where(q => q.name.ToString().Contains(textbox.Text)).ToList();
                    break;
                case 1:
                    PageGrid.ItemsSource = SourceCore.db.models.Where(q => q.year.ToString().Contains(textbox.Text)).ToList();
                    break;
            }
        }

        private void AddCommit_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (string.IsNullOrEmpty(NameTextBox.Text)) errors.AppendLine("Укажите название");
            if (string.IsNullOrEmpty(YearTextBox.Text)) errors.AppendLine("Укажите год");
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            if (DlgMode == 0)
            {
                try
                {
                    var NewBase = new Base.models();
                    NewBase.name = NameTextBox.Text.Trim();
                    NewBase.year = int.Parse(YearTextBox.Text.Trim());
                    SourceCore.db.models.Add(NewBase);
                    SelectedModels = NewBase;
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
                    var EditBase = new Base.models();
                    EditBase = SourceCore.db.models.First(p => p.id == SelectedModels.id);
                    EditBase.name = NameTextBox.Text.Trim();
                    EditBase.year = int.Parse(YearTextBox.Text.Trim());
                }
                catch (Exception)
                {
                    MessageBox.Show("Введены некоректные данные");
                }
            }
            try
            {
                SourceCore.db.SaveChanges();
                UpdateGrid(SelectedModels);
                DlgLoad(false, "");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void AddRollback_Click(object sender, RoutedEventArgs e)
        {
            UpdateGrid(SelectedModels);
            DlgLoad(false, "");
        }
    }
}
