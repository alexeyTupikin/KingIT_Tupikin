using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace KingIT.pages.ManagerC
{
    /// <summary>
    /// Логика взаимодействия для HallsList.xaml
    /// </summary>
    public partial class HallsList : Page
    {
        MainWindow main;
        malls currentMall;
        public HallsList(MainWindow _main, malls _currentMall)
        {
            InitializeComponent();
            main = _main;
            main.Title = "Павильоны";
            currentMall = _currentMall;
            FillDataGrid();
            FillFloorBox();
            FillStatusBox();
        }
        private void FillFloorBox()
        {
            try
            {
                using (var db = new KingITDBEntities1(main.connectionName))
                {
                    var list = (from h in db.halls
                                where h.idMall == currentMall.idMall
                                orderby h.floor
                                select h.floor.ToString()).Distinct().ToList();
                    list.Add("...");
                    FloorCB.ItemsSource = list;
                }
            } 
            catch
            {
                MessageBox.Show("Ошибка подключения к бд");
            }
        }
        private void FillStatusBox()
        {
            StatusCB.Items.Add("Арендован");
            StatusCB.Items.Add("Свободен");
            StatusCB.Items.Add("Забронирован");
            StatusCB.Items.Add("...");
        }
        private void FillDataGrid()
        {
            try
            {
                using (var db = new KingITDBEntities1(main.connectionName))
                {
                    DG.ItemsSource = (from h in db.getHalls(currentMall.idMall) select h).ToList();
                }
            }
            catch { }
        }
        private void Exit(object sender, RoutedEventArgs e)
        {
            main.frame.Navigate(new pages.iManagerC(main));
        }
        private void Add(object sender, RoutedEventArgs e)
        {
            main.frame.Navigate(new EditHallList(main, currentMall));
        }
        private void Edit(object sender, RoutedEventArgs e)
        {
            var selected = (getHalls_Result)DG.SelectedItem;
            try
            {
                using (var db = new KingITDBEntities1(main.connectionName))
                {
                    var edited = (from h in db.halls
                                  where h.hallNum == selected.hallNum &&
                                  h.idMall == selected.idMall
                                  select h).FirstOrDefault();
                    main.frame.Navigate(new EditHallList(main, edited, currentMall));
                }
            }
            catch (Exception ex) { MessageBox.Show("Ошибка открытия окна\n" + ex.Message); }
            FillDataGrid();

        }
        private void Delete(object sender, RoutedEventArgs e)
        {
            var selected = (getHalls_Result)DG.SelectedItem;
            try
            {
                using (var db = new KingITDBEntities1(main.connectionName))
                {
                    var deleted = (from h in db.halls
                                   where h.idHall == selected.idHall
                                   select h).FirstOrDefault();
                    if (deleted.status == 5)
                    {
                        deleted.status = 3;
                        db.SaveChanges();
                        FillDataGrid();
                    }
                    else MessageBox.Show("Павильон арендован или забронирован");
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        private void DoFilters(string floor, decimal min, decimal max, string status_title)
        {
            try
            {
                using (var db = new KingITDBEntities1(main.connectionName))
                {
                    int ifloor = 0;
                    if (floor != "...")
                    {
                        ifloor = Convert.ToInt32(floor);
                    }
                    DG.ItemsSource = (from h in db.getHalls(currentMall.idMall)
                                      where h.floor == ((floor == "...") ? h.floor : ifloor) &&
                                      h.area >= ((min >= 0 && min < max) ? min : 0) &&
                                      h.area <= ((max >= min && max <= 500) ? max : 500) &&
                                      h.hall_status == ((status_title == "...") ? h.hall_status : status_title)
                                      select h).ToList();
                }
            }
            catch (Exception ex) { MessageBox.Show("ошибка " + ex.Message); }
        }
        private void DG_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            DelButton.IsEnabled = true;
            EditButton.IsEnabled = true;
        }
        private void CB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                DoFilters(
                FloorCB.SelectedIndex == -1 ? "..." : Convert.ToString(FloorCB.SelectedValue),
                decimal.TryParse(AreaBoxMin.Text, out var i) ? Convert.ToDecimal(AreaBoxMin.Text) : 0,
                decimal.TryParse(AreaBoxMax.Text, out
                i) ? Convert.ToDecimal(AreaBoxMax.Text) : 500,
                StatusCB.SelectedIndex == -1 ? "..." : Convert.ToString(StatusCB.SelectedValue));
            }
            catch { }
        }
        private void AreaBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (FloorCB != null)
            {
                if (StatusCB != null)
                {
                    DoFilters(
                    FloorCB.SelectedIndex == -1 ? "..." : Convert.ToString(FloorCB.SelectedValue),
                    decimal.TryParse(AreaBoxMin.Text, out var i) ? Convert.ToDecimal(AreaBoxMin.Text) : 0,
                    decimal.TryParse(AreaBoxMax.Text, out i) ? Convert.ToDecimal(AreaBoxMax.Text) : 500,
                    StatusCB.SelectedIndex == -1 ? "..." : Convert.ToString(StatusCB.SelectedValue));
                }
            }
        }

        private void rent_add(object sender, RoutedEventArgs e)
        {
            if (DG.SelectedIndex != -1)
            {
                var selected = (getHalls_Result)DG.SelectedItem;
                try
                {
                    using (var db = new KingITDBEntities1(main.connectionName))
                    {
                        var rented = (from h in db.halls
                                      where h.idHall == selected.idHall
                                      select h).FirstOrDefault();
                        var rent = new addRent(main, rented, currentMall);
                        rent.ShowDialog();
                        FillDataGrid();
                    }
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
            }
        }
    }
}   
