using stroyinvest.Model;
using stroyinvest.ViewModel;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace stroyinvest.View
{
    public partial class ObjectDetailsPage : Page
    {
        int userRole = Properties.Settings.Default.idRole;
        Core db = new Core();
        Objects currentObject;
        public ObjectDetailsPage(Objects selectedObject)
        {
            InitializeComponent();
            DataContext = selectedObject; // Устанавливаем объект как источник данных для страницы
            selectedObject.ObjectTypes.ObjectTypeName = selectedObject.ObjectTypes.ObjectTypeName;
            selectedObject.Users.UserBuilderName = selectedObject.Users.UserBuilderName;
            MainImage.Source =  new BitmapImage(new Uri(selectedObject.ObjectPhotoPath, UriKind.RelativeOrAbsolute));
            currentObject = selectedObject;
            if (userRole == 1)
            {
                AdminPanel.Visibility = System.Windows.Visibility.Visible;
            }
        }

        private void RemoveObjectButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                ObjectVM.RemoveObject(currentObject);

            }
            catch {
                MessageBox.Show("Произошла непредвиденная ошибка");
            }
            this.NavigationService.Navigate(new ObjectListViewPage());
        }

        private void EditObjectButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button) {
                Objects editObject = button.DataContext as Objects;
                this.NavigationService.Navigate(new ObjectEditPage(editObject));
                    }
        }

    }
}