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
using System.Data.Entity;

namespace KingIT.pages
{
    /// <summary>
    /// Логика взаимодействия для iManagerC.xaml
    /// </summary>
    public partial class iManagerC : Page
    {
        MainWindow mainWindow;
        public iManagerC(MainWindow _mainWindow)
        {
            InitializeComponent();
            mainWindow = _mainWindow;
            dataGridUpdate();
            
        }

        public void dataGridUpdate()
        {
            using (KingITDBEntities cnt = new KingITDBEntities())
            {
                var data = (from m in cnt.forMalls orderby m.address, m.StatusMall select m);
                dataGridMalls.ItemsSource = data.ToList();
                f_Status.Items.Clear();
                f_Status.Items.Add("План");
                f_Status.Items.Add("Реализация");
                f_Status.Items.Add("Строительство");
                f_city.ItemsSource = (from m in cnt.malls select m.address).Distinct().ToList();
            }
        }

        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            using (KingITDBEntities cnt = new KingITDBEntities())
            {
                cnt.SaveChanges();
                dataGridUpdate();
            }
        }

        private void goIMalls_but_Click(object sender, RoutedEventArgs e)
        {
            //реализовать переход на форму интерфес тц
        }

        private void f_Status_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            using (KingITDBEntities cnt = new KingITDBEntities())
            {
                string buf_string = Convert.ToString(f_Status.SelectedValue);

            }
        }
    }
}
