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
        string form = "";
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

        public void update_proc()
        {
            using (KingITDBEntities cnt = new KingITDBEntities())
            {
                cnt.SaveChanges();
                if ((f_Status.SelectedValue == null) && (f_city.SelectedValue != null))
                {
                    update_w_f_city();
                }
                else if ((f_city.SelectedValue == null) && (f_Status.SelectedValue != null))
                {
                    update_w_f_status();
                }
                else if ((f_Status.SelectedValue == null) && (f_city.SelectedValue == null))
                {
                    dataGridUpdate();
                }
            }
        }

        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            update_proc();
        }

        private void add_but_Click(object sender, RoutedEventArgs e)
        {
            form = "add";
            addNewMall addNewMallWindow = new addNewMall(form);
            addNewMallWindow.ShowDialog();
            if(addNewMallWindow.IsActive == false)
            {
                update_proc();
            }
        }

        private void f_Status_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            update_w_f_status();
        }

        public void update_w_f_status()
        {
            using (KingITDBEntities cnt = new KingITDBEntities())
            {
                string buf_status = Convert.ToString(f_Status.SelectedValue);
                if (f_city.SelectedValue != null)
                {
                    string buf_city = Convert.ToString(f_city.SelectedValue);
                    using (KingITDBEntities cnt12 = new KingITDBEntities())
                    {
                        var data = (from m in cnt12.forMalls where m.StatusMall == buf_status && m.address == buf_city orderby m.address, m.StatusMall select m);
                        dataGridMalls.ItemsSource = data.ToList();
                    }
                }
                else
                {
                    using (KingITDBEntities cnt12 = new KingITDBEntities())
                    {
                        var data = (from m in cnt12.forMalls where m.StatusMall == buf_status orderby m.address, m.StatusMall select m);
                        dataGridMalls.ItemsSource = data.ToList();
                    }
                }
            }
        }

        public void update_w_f_city()
        {
            using (KingITDBEntities cnt = new KingITDBEntities())
            {
                string buf_city = Convert.ToString(f_city.SelectedValue);
                if (f_Status.SelectedValue != null)
                {
                    string buf_status = Convert.ToString(f_Status.SelectedValue);
                    using (KingITDBEntities cnt12 = new KingITDBEntities())
                    {
                        var data = (from m in cnt12.forMalls where m.StatusMall == buf_status && m.address == buf_city orderby m.address, m.StatusMall select m);
                        dataGridMalls.ItemsSource = data.ToList();
                    }
                }
                else
                {
                    using (KingITDBEntities cnt12 = new KingITDBEntities())
                    {
                        var data = (from m in cnt12.forMalls where m.address == buf_city orderby m.address, m.StatusMall select m);
                        dataGridMalls.ItemsSource = data.ToList();
                    }
                }
            }
        }


        private void f_city_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            update_w_f_city();
        }

        private void butt_def_filters_Click(object sender, RoutedEventArgs e)
        {
            f_Status.SelectedValue = null;
            f_city.SelectedValue = null;
            dataGridUpdate();
        }

        private void delete_button_Click(object sender, RoutedEventArgs e)
        {
            var selObject = (forMalls)dataGridMalls.SelectedItem;
            try
            {
                using (KingITDBEntities cnt = new KingITDBEntities())
                {
                    malls delObject = (from m in cnt.malls
                                       where m.idMall == selObject.idMall
                                       select m).FirstOrDefault();
                    delObject.idStatus = 3;
                    cnt.SaveChanges();
                    if((f_Status.SelectedValue == null)&& (f_city.SelectedValue != null))
                    {
                        update_w_f_city();
                    }
                    else if((f_city.SelectedValue == null) && (f_Status.SelectedValue != null))
                    {
                        update_w_f_status();
                    }
                    else if ((f_Status.SelectedValue == null) && (f_city.SelectedValue == null))
                    {
                        dataGridUpdate();
                    }
                }
            }
            catch (Exception X)
            {
                MessageBox.Show("Ошибка"+ X.Message);
            }
        }

        private void edit_button_Click(object sender, RoutedEventArgs e)
        {
            form = "edit";
            addNewMall editNewMallWindow = new addNewMall((forMalls)(dataGridMalls.SelectedItem), mainWindow, form);
            editNewMallWindow.ShowDialog();
            if (editNewMallWindow.IsActive == false)
            {
                update_proc();
            }
        }
    }
}
