using System.Linq;
using System.Windows;
using System.Windows.Controls;
namespace BibFond
{
    public partial class MyBookWindow : Window
    {
        public static Base.users user = MainWindow.user;
        public Base.myBooklist SelectedBook;
        
        public MyBookWindow()
        {
            InitializeComponent();
            if (user != null)
            {
                ShowUserStackPanel();
            }
             booksList.ItemsSource = SourceCore.Base.myBooklist.SqlQuery($"select * from myBooklist where userId = {user.id}").ToList();
        }

        private void booksList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Base.myBooklist p = (Base.myBooklist)booksList.SelectedItem;            
        }

        private void ShowUserStackPanel()
        {
            UserStackPanel.Visibility = Visibility.Visible;
            NameUser.Text = user.name;
            if (user.admin) AdminPanelButton.Visibility = Visibility.Visible;
        }

        private void AdminPanel_Click(object sender, RoutedEventArgs e)
        {
            WindowManager.ChangeWindow("AdminWindow", this);
        }

        private void Main_Click(object sender, RoutedEventArgs e)
        {
            WindowManager.ChangeWindow("MainWindow", this);
        }
    }
}