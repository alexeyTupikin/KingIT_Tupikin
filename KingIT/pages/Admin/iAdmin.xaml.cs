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

namespace KingIT.pages.Admin
{
    /// <summary>
    /// Логика взаимодействия для iAdmin.xaml
    /// </summary>
    public partial class iAdmin : Page
    {
        MainWindow mainWindow;
        public string w_form = "";
        public iAdmin(MainWindow _mainWindow)
        {
            InitializeComponent();
            mainWindow = _mainWindow;
            dataGridUpdate();
        }

        public void dataGridUpdate()
        {
            using (KingITDBEntities1 cnt = new KingITDBEntities1(mainWindow.connectionName))
            {
                var data = (from emp in cnt.for_employers select emp);
                dataGridEmp.ItemsSource = data.ToList();
            }
        }

        private void add_but_Click(object sender, RoutedEventArgs e)
        {
            w_form = "add";
            addEmployer add_emp = new addEmployer(mainWindow, w_form);
            add_emp.ShowDialog();
            if(add_emp.IsActive == false)
            {
                dataGridUpdate();
            }
        }

        private void edit_but_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridEmp.SelectedItem != null)
            {
                using (KingITDBEntities1 cnt = new KingITDBEntities1())
                {
                    w_form = "edit";
                    var sel_emp = (for_employers)dataGridEmp.SelectedItem;
                    employers cur_emp = (from emp in cnt.employers where emp.idEmployer == sel_emp.idEmployer select emp).FirstOrDefault();
                    addEmployer add_emp = new addEmployer(mainWindow, w_form, cur_emp);
                    add_emp.ShowDialog();
                    if (add_emp.IsActive == false)
                    {
                        dataGridUpdate();
                    }
                }
            }
            else MessageBox.Show("Для того что бы приступить к редактированию необходимо выбрать элемент из списка.");
        }
    }
}
