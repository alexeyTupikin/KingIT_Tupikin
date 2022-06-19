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
using System.Data.SqlClient;
using System.IO;
using System.Data;

namespace KingIT
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<string> FIO = new List<string>();
        public MainWindow()
        {
            InitializeComponent();
            
            //setPhotoEmp();
            //setPhotoMall();
        }

        #region setPhoto
        public void setPhotoEmp()
        {
            string[] fio;
            string last_name = "";
            string name = "";
            string second_name = "";
            int buf_id;
            char[] end_name = { '.', 'j', 'p', 'g', 'n' }; 
            string pathToDirectory = @"C:\Users\alexe\Source\Repos\KingIT_Tupikin\KingIT\img_employers";
            Directory
                .GetFiles(pathToDirectory, "*", SearchOption.TopDirectoryOnly).ToList()
                .ForEach(f =>
                {
                fio = (System.IO.Path.GetFileName(f.TrimEnd(end_name))).Split();
                    last_name = fio[0];
                    name = fio[1];
                    second_name = fio[2];
                using (KingITDBEntities cnt = new KingITDBEntities())
                {
                        buf_id = (from e in cnt.employers where e.lastName == last_name && 
                                  e.name == name && e.middleName == second_name 
                                  select e.idEmployer).FirstOrDefault();

                        var current_ = (from e in cnt.employers where e.idEmployer == buf_id select e).FirstOrDefault();
                        current_.photo = get_set_img.GetImageBytes(System.IO.Path.GetFullPath(f));
                        cnt.SaveChanges();
                    }
                });
        }

        public void setPhotoMall()
        {
            string title_mall;
            int buf_id;
            char[] end_name = { '.', 'j', 'p', 'g', 'n' };
            string pathToDirectory = @"C:\Users\alexe\Source\Repos\KingIT_Tupikin\KingIT\img_mall";
            Directory
                .GetFiles(pathToDirectory, "*", SearchOption.TopDirectoryOnly).ToList()
                .ForEach(f =>
                {
                    title_mall = System.IO.Path.GetFileName(f.TrimEnd(end_name));
                    using (KingITDBEntities cnt1 = new KingITDBEntities())
                    {
                        buf_id = (from e in cnt1.malls
                                  where e.title == title_mall
                                  select e.idMall).FirstOrDefault();

                        var current_ = (from e in cnt1.malls where e.idMall == buf_id select e).FirstOrDefault();
                        if (current_ != null) {
                            current_.icon = get_set_img.GetImageBytes(System.IO.Path.GetFullPath(f));
                            cnt1.SaveChanges();
                        }
                    }
                });
        }
        #endregion
    }
}
