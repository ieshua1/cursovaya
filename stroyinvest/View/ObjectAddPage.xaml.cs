using cursed.ViewModel;
using Microsoft.Win32;
using stroyinvest.Model;
using stroyinvest.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace stroyinvest.View
{
    public partial class ObjectAddPage : Page
    {
        Core db = new Core();
        public ObjectAddPage()
        {
            InitializeComponent();
            ObjectTypeCBox.ItemsSource = db.context.ObjectTypes.Select(x => x.ObjectTypeName).ToList();
            ObjectBuilderCBox.ItemsSource = db.context.Users.Where(x => x.UserBuilderName != null).Select(x => x.UserBuilderName).ToList();
            ObjectStatusCBox.ItemsSource = db.context.ObjectStatuses.Select(x => x.ObjectStatusName).ToList();
        }

        private async void UploadPhoto(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "Image Files(*.JPG;*.JPEG;*.PNG;*.GIF)|*.JPG;*.JPEG;*.PNG;*.GIF|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 0;
            openFileDialog1.RestoreDirectory = true;
            if (openFileDialog1.ShowDialog() == true)
            {
                try
                {
                    string path = openFileDialog1.FileName;
                    string filename = openFileDialog1.SafeFileName;
                    var uploader = new FilesVM();
                    string imageUrl = await uploader.UploadImage(path);
                    BitmapImage objectImage = new BitmapImage(new Uri(imageUrl));
                    PhotoImage.Source = objectImage;
                }
                catch
                {
                    MessageBox.Show("Невозможно загрузить изображение");
                }
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ObjectVM.CreateNewObject(
                    objectName: ObjectNameTBox.Text,
                    objectPrice: Convert.ToInt32(ObjectPriceTBox.Text),
                    objectTypeId: db.context.ObjectTypes.Where(x => x.ObjectTypeName == ObjectTypeCBox.Text).FirstOrDefault().IdObjectType,
                    objectRoomCount: Convert.ToInt32(ObjectRoomCountTBox.Text),
                    objectSquare: Convert.ToInt32(ObjectSquareTBox.Text),
                    objectDescription: ObjectDescTBox.Text,
                    objectAddress: ObjectAddressTBox.Text,
                    objectBuilderId: db.context.Users.Where(x => x.UserBuilderName == ObjectBuilderCBox.Text).FirstOrDefault().IdUser,
                    objectStatusId: db.context.ObjectStatuses.Where(x => x.ObjectStatusName == ObjectStatusCBox.Text).FirstOrDefault().IdObjectStatus,
                    objectPhotoPath: PhotoImage.Source != null ? PhotoImage.Source.ToString() : null
                    );
                MessageBox.Show("Объект успешно добавлен");
                this.NavigationService.GoBack();
            }
            catch
            {
                MessageBox.Show("Во время добавления объекта произошла ошибка, проверьте корректность всех данных и попробуйте ещё раз");
            }
        }
    }
}