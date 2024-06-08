using stroyinvest.View;
using System;
using System.Windows;
using System.Windows.Navigation;

namespace stroyinvest
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Navigate(new ObjectListViewPage());
            UpdateButtons();
        }

        private void AuthButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new LoginPage());
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.idUser = 0;
            Properties.Settings.Default.idRole = 0;
            Properties.Settings.Default.Save();
            UpdateButtons();
            MainFrame.NavigationService.Refresh();
        }

        private void MainFrame_ContentRendered(object sender, EventArgs e)
        {
            UpdateButtons();
        }

        private void MainFrame_Navigated(object sender, NavigationEventArgs e)
        {
            UpdateButtons();
        }

        private void UpdateButtons()
        {
            AuthButton.Visibility = Properties.Settings.Default.idUser == 0
                ? Visibility.Visible
                : Visibility.Collapsed;

            ExitButton.Visibility = Properties.Settings.Default.idUser != 0
                ? Visibility.Visible
                : Visibility.Collapsed;

            BackButton.Visibility = MainFrame.CanGoBack 
                ? Visibility.Visible 
                : Visibility.Collapsed;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (MainFrame.CanGoBack)
            {
                MainFrame.GoBack();

            }
            UpdateButtons();
        }

    }
}