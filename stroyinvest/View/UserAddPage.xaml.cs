using stroyinvest.Model;
using stroyinvest.ViewModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace stroyinvest.View
{
    /// <summary>
    /// Логика взаимодействия для UserAddPage.xaml
    /// </summary>
    public partial class UserAddPage : Page
    {
        Core db = new Core();
        public UserAddPage()
        {
            InitializeComponent();

            UserRoleCBox.ItemsSource = db.context.UserRoles.Select(x => x.UserRoleName).ToList();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if(UserRoleCBox.SelectedIndex == -1)
            {
                MessageBox.Show("Выберите роль пользователя");
                return;
            }
            try
            {
                int _role = db.context.UserRoles.Where(x => x.UserRoleName == UserRoleCBox.Text).FirstOrDefault().IdUserRole;

                UserVM.AddUser(
                    UserFirstNameTBox.Text,
                    UserLastNameTBox.Text,
                    UserEmailTBox.Text,
                    UserPasswordPBox.Password,
                    _role,
                    UserBuilderNmaeTBox.Text != null ? UserBuilderNmaeTBox.Text : null,
                    UserPatronymicTBox.Text != null ? UserPatronymicTBox.Text : null
                );
                db.context.SaveChanges();
                MessageBox.Show("Пользователь успешно добавлен");
                this.NavigationService.GoBack();
            }
            catch
            {
                MessageBox.Show("Во время добавления пользователя произошла непредвиденная ошибка");
            }
        }

        private void UserRoleCBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(UserRoleCBox.SelectedValue.ToString() == "Застройщик") {
                BuilderStackPanel.Visibility = Visibility.Visible;
            }
            else
            {
                BuilderStackPanel.Visibility = Visibility.Collapsed;
            }
        }
    }
}
