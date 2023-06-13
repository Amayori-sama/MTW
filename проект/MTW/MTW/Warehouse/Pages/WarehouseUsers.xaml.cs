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
    /// Interaction logic for WarehouseUsers.xaml
    /// </summary>
    public partial class WarehouseUsers : Page
    {
        private int DlgMode;
        private Base.users SelectedUsers;
        private ObservableCollection<Base.users> Users;

        public WarehouseUsers()
        {
            InitializeComponent();
            DataContext = this;
            DlgLoad(false, "");
            UpdateGrid(null);
        }

        private void LoadingUsers(object sender, RoutedEventArgs e)
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
        private void UpdateGrid(Base.users user)
        {
            if ((user == null) && (PageGrid.ItemsSource != null))
            {
                user = (Base.users)PageGrid.SelectedItem;
            }
            Users = new ObservableCollection<Base.users>(SourceCore.db.users);

            PageGrid.ItemsSource = Users;
            PageGrid.ItemsSource = SourceCore.db.users.ToList();
            PageGrid.SelectedItem = user;
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
            LoginTextBox.Text = SelectedUsers.login;
            PasswordTextBox.Text = SelectedUsers.password;
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
                SelectedUsers = (Base.users)PageGrid.SelectedItem;
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
                SelectedUsers = (Base.users)PageGrid.SelectedItem;
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
                    Base.users DeletingAccessory = (Base.users)PageGrid.SelectedItem;
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
                    Base.users SelectingAccessory = (Base.users)PageGrid.SelectedItem;
                    SourceCore.db.users.Remove(DeletingAccessory);
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
                    PageGrid.ItemsSource = SourceCore.db.users.Where(q => q.login.ToString().Contains(textbox.Text)).ToList();
                    break;
                case 1:
                    PageGrid.ItemsSource = SourceCore.db.users.Where(q => q.password.ToString().Contains(textbox.Text)).ToList();
                    break;
            }
        }

        private void AddCommit_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (string.IsNullOrEmpty(LoginTextBox.Text)) errors.AppendLine("Укажите логин");
            if (string.IsNullOrEmpty(PasswordTextBox.Text)) errors.AppendLine("Укажите пароль");
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            if (DlgMode == 0)
            {
                try
                {
                    var NewBase = new Base.users();
                    NewBase.login = LoginTextBox.Text.Trim();
                    NewBase.password = PasswordTextBox.Text.Trim();
                    SourceCore.db.users.Add(NewBase);
                    SelectedUsers = NewBase;
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
                    var EditBase = new Base.users();
                    EditBase = SourceCore.db.users.First(p => p.id == SelectedUsers.id);
                    EditBase.login = LoginTextBox.Text.Trim();
                    EditBase.password = PasswordTextBox.Text.Trim();
                }
                catch (Exception)
                {
                    MessageBox.Show("Введены некоректные данные");
                }
            }
            try
            {
                SourceCore.db.SaveChanges();
                UpdateGrid(SelectedUsers);
                DlgLoad(false, "");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void AddRollback_Click(object sender, RoutedEventArgs e)
        {
            UpdateGrid(SelectedUsers);
            DlgLoad(false, "");
        }
    }
}