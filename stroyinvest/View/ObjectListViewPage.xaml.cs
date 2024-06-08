using stroyinvest.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace stroyinvest.View
{
    /// <summary>
    /// Логика взаимодействия для ObjectListViewPage.xaml
    /// </summary>
    public partial class ObjectListViewPage : Page
    {
        private List<Objects> _allObjects = new List<Objects>();
        Core db = new Core();
        public ObjectListViewPage()
        {
            InitializeComponent();
            LoadObjects();
        }

        private void LoadObjects()
        {
          
                _allObjects = db.context.Objects.ToList();
                ObjectsListView.ItemsSource = _allObjects;
                ObjectsListView.UnselectAll();
        }

        // Обработчик кнопки "Добавить объект"
        private void AddObjectButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ObjectAddPage());
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            // Проверка роли текущего пользователя
            if (Properties.Settings.Default.idRole != 1 && // Роль администратора
                Properties.Settings.Default.idRole != 2)   // Роль менеджера
            {
                AddObjectButton.Visibility = Visibility.Collapsed; // Скрыть кнопку
                UserManagmentButton.Visibility = Visibility.Collapsed;
            }
            else
            {
                AddObjectButton.Visibility = Visibility.Visible;
                UserManagmentButton.Visibility = Visibility.Visible;
            }
            LoadObjects();
        }

        private void ObjectsListView_Selected(object sender, RoutedEventArgs e)
        {
            if (ObjectsListView.SelectedItem is Objects selectedObject)
            {
                NavigationService.Navigate(new ObjectDetailsPage(selectedObject));
            }
        }

        private void UserManagmentButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new UserManagmentPage());
        }
    }
}