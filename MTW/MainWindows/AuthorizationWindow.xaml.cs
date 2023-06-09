using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MTW.MainWindows
{
    /// <summary>
    /// Interaction logic for AuthorizationWindow.xaml
    /// </summary>
    public partial class AuthorizationWindow : Window
    {
        public AuthorizationWindow()
        {
            InitializeComponent();
        }

        private void AuthCommit(object sender, RoutedEventArgs e)
        {
            Base.users user = SourceCore.db.users.SingleOrDefault(U => U.login == LoginText.Text && U.password == PasswordText.Text);
            if (user != null)
            {
                Warehouse.WarehouseWindow wh = new Warehouse.WarehouseWindow();
                Close();
                wh.Show();
            }
            else
            {
                MessageBox.Show("Неверно указан логин и/или пароль!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
            };
        }

        private void AuthRollBack(object sender, RoutedEventArgs e)
        {
            WelcomeWindow ww = new WelcomeWindow();
            Close();
            ww.Show();
        }
    }
}