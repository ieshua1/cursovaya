using stroyinvest.Model;
using stroyinvest.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
    /// Логика взаимодействия для UserManagmentPage.xaml
    /// </summary>
    public partial class UserManagmentPage : Page
    {
        List<Users> userList = new List<Users>();
        UserManagementVM _viewModel = new UserManagementVM();
        Core db = new Core();

        public UserManagmentPage()
        {
            InitializeComponent();
           UpdateList();
        }

        private void AddUserButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new UserAddPage());
            UpdateList();

        }

        private void RemoveUserButton_Click(object sender, RoutedEventArgs e)
        {
            if(sender is Button button)
            {
                Users delUser = button.DataContext as Users;
                if (MessageBox.Show($"Вы уверены, что хотите удалить пользователя {delUser.UserFirstName} {delUser.UserLastName}?", "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    _viewModel.DeleteUser(delUser);
                }
            }
            UpdateList();

        }

        private void UpdateList()
        {
            userList = db.context.Users.ToList();
            UsersDataGrid.ItemsSource = userList;
        }

        private void UsersGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            if (e.EditAction == DataGridEditAction.Commit)
            {
                var editedRow = e.Row.Item as Users;
                db.context.Entry(editedRow).State = EntityState.Modified;
                db.context.SaveChanges();
            }
            UpdateList();
        }

        private void Page_Initialized(object sender, EventArgs e)
        {
            UpdateList();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateList();

        }
    }
}
