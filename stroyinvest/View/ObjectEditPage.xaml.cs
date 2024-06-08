using cursed.ViewModel;
using Microsoft.Win32;
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
    /// Логика взаимодействия для ObjectEditPage.xaml
    /// </summary>
    public partial class ObjectEditPage : Page
    {
        Core db = new Core();
        Objects selObject;
        public ObjectEditPage(Objects editObject)
        {
            InitializeComponent();

            selObject = editObject;

            ObjectTypeCBox.ItemsSource = db.context.ObjectTypes.Select(x => x.ObjectTypeName).ToList();
            ObjectBuilderCBox.ItemsSource = db.context.Users.Where(x => x.UserBuilderName != null).Select(x => x.UserBuilderName).ToList();
            ObjectStatusCBox.ItemsSource = db.context.ObjectStatuses.Select(x => x.ObjectStatusName).ToList();

            ObjectNameTBox.Text = editObject.ObjectName;
            ObjectPriceTBox.Text = editObject.ObjectPrice.ToString();
            ObjectRoomCountTBox.Text = editObject.ObjectRoomCount.ToString();
            ObjectSquareTBox.Text = editObject.ObjectSquare.ToString();
            ObjectDescTBox.Text = editObject.ObjectDescription;
            ObjectAddressTBox.Text = editObject.ObjectAddress;
            PhotoImage.Source = new BitmapImage( new  Uri(editObject.ObjectPhotoPath));


            ObjectTypeCBox.SelectedIndex = db.context.ObjectTypes.Where(x => x.IdObjectType == editObject.ObjectTypeId).Select(x => x.IdObjectType).FirstOrDefault()-1;
            ObjectStatusCBox.SelectedIndex = db.context.ObjectStatuses.Where(x => x.IdObjectStatus == editObject.ObjectStatusId).Select(x => x.IdObjectStatus).FirstOrDefault()-1;
            
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Objects curObject = db.context.Objects.Where(x => x.IdObject == selObject.IdObject).FirstOrDefault();
                curObject.ObjectName = ObjectNameTBox.Text;
                curObject.ObjectPrice = Convert.ToInt32(ObjectPriceTBox.Text);
                curObject.ObjectTypeId = db.context.ObjectTypes.Where(x => x.ObjectTypeName == ObjectTypeCBox.Text).FirstOrDefault().IdObjectType;
                curObject.ObjectRoomCount = Convert.ToInt32(ObjectRoomCountTBox.Text);
                curObject.ObjectSquare = Convert.ToInt32(ObjectSquareTBox.Text);
                curObject.ObjectDescription = ObjectDescTBox.Text;
                curObject.ObjectAddress = ObjectAddressTBox.Text;
                curObject.ObjectBuilderId = db.context.Users.Where(x => x.UserBuilderName == ObjectBuilderCBox.Text).FirstOrDefault().IdUser;
                curObject.ObjectStatusId = db.context.ObjectStatuses.Where(x => x.ObjectStatusName == ObjectStatusCBox.Text).FirstOrDefault().IdObjectStatus;
                curObject.ObjectPhotoPath = PhotoImage.Source != null ? PhotoImage.Source.ToString() : null;
                db.context.SaveChanges();
                this.NavigationService.Navigate(new ObjectListViewPage());
            }
            catch
            {
                MessageBox.Show("Во время редактирования произошла непредвиденная ошибка, проверьте корректность данных");
            }

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
    }
}
