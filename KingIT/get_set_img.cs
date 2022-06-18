using System.IO;
using System.Windows.Media.Imaging;

namespace KingIT
{
    public class get_set_img
    {
        public static byte[] GetImageBytes(string filePath) //из изображения в байты
        {
            byte[] photo = null;
            using (FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                using (BinaryReader reader = new BinaryReader(stream))
                {
                    photo = reader.ReadBytes((int)stream.Length);
                }
            }
            return photo;
        }

        public static BitmapImage BytesToImage(byte[] bytes) //из байтов в изображение
        {
            BitmapImage image = null;
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                image = new BitmapImage();
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.StreamSource = ms;
                image.EndInit();
            }
            return image;
        }
    }
}
