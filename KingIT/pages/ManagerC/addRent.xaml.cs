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
using System.Windows.Shapes;
using KingIT.Model;

namespace KingIT.pages.ManagerC
{
    /// <summary>
    /// Логика взаимодействия для RentHall.xaml
    /// </summary>
    public partial class addRent : Window
    {
        MainWindow main;
        halls currentHall;
        malls currentMall;


        public addRent(MainWindow _main, halls _currentHall, malls _currentMall)
        {
            InitializeComponent();
            main = _main;
            currentHall = _currentHall;
            currentMall = _currentMall;
            FillTenantBox();
            blockDates();
        }
        private void FillTenantBox()
        {
            try
            {
                using (var db = new KingITDBEntities1(main.connectionName))
                {
                    TenantBox.ItemsSource = (from t in db.tenants select t.title).ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void blockDates()
        {
            try
            {
                using (var db = new KingITDBEntities1())
                {
                    var lastdate = new CalendarDateRange() { End = DateTime.MinValue };
                    foreach (rent current in db.rent)
                    {
                        DateEnd.BlackoutDates.Add(new CalendarDateRange() { Start = current.dateStart, End = current.dateEnd });
                        DateStart.BlackoutDates.Add(new CalendarDateRange() { Start = current.dateStart, End = current.dateEnd });
                        if (lastdate.End < current.dateEnd) lastdate.End = current.dateEnd;
                    }
                    DateStart.DisplayDate = lastdate.End;
                    DateEnd.DisplayDate = lastdate.End;
                }
            }
            catch { }

        }
        private void Exit(object sender, RoutedEventArgs args)
        {
            this.Close();
        }
        private void Rent(object sender, RoutedEventArgs args)
        {
            try
            {
                using (var db = new KingITDBEntities1(main.connectionName))
                {
                    var start = DateStart.SelectedDate.Value;
                    var end = DateEnd.SelectedDate.Value;
                    MessageBox.Show(DateTime.Now.DayOfYear.ToString());
                    var tenant_id = (from t in db.tenants
                                     where t.title == TenantBox.SelectedValue.ToString()
                                     select t.idTenant).First();
                    db.add_rent(currentHall.idHall, (start.DayOfYear == DateTime.Now.DayOfYear) ? true : false,
                    currentHall.hallNum, currentHall.idMall, start, end, tenant_id, main.employer_id);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.Message);
            }
        }
    }
}