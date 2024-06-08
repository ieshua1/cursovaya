using System;
using System.Windows.Media.Imaging;

namespace stroyinvest.Model
{
    public class Core
    {
        public stroyinvestEntities context = new stroyinvestEntities();
    }

    public partial class Objects
    {
        public BitmapImage FullImagePath
        {
            get
            {
                if (string.IsNullOrEmpty(ObjectPhotoPath)) return null;
                return new BitmapImage(new Uri(@"..\..\Resources\Images" + ObjectPhotoPath, UriKind.Relative));
            }
        }
    }
}
