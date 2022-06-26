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
using KingIT.Model;

namespace KingIT.pages.ManagerA
{
    /// <summary>
    /// Логика взаимодействия для iManagerA.xaml
    /// </summary>
    public partial class iManagerA : Page
    {
        MainWindow mainWindow;
        public iManagerA(MainWindow _mainWindow)
        {
            InitializeComponent();
            mainWindow = _mainWindow;
            dataGridUpdate();
        }

        public void dataGridUpdate()
        {
            using (KingITDBEntities1 cnt = new KingITDBEntities1())
            {
                var data = (from p in cnt.for_managerA_proc() select p).ToList();
                dataGrid.ItemsSource = data;
            }
        }
    }
}
