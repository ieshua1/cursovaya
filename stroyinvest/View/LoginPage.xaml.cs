using stroyinvest.Model;
using stroyinvest.ViewModel;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace stroyinvest.View
{
    /// <summary>
    /// Логика взаимодействия для LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        Core db = new Core();
        public LoginPage()
        {
            InitializeComponent();
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bool result = UserVM.AuthCheck(TBoxEmail.Text, PBoxPassword.Password);
                if (result)
                {
                    Users item = db.context.Users.Where(x => x.UserEmail == TBoxEmail.Text && x.UserPassword == PBoxPassword.Password).FirstOrDefault();
                    Properties.Settings.Default.idUser = item.IdUser;
                    Properties.Settings.Default.idRole = item.UserRoleId;
                    Properties.Settings.Default.Save();
                    this.NavigationService.Navigate(new ObjectListViewPage());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
