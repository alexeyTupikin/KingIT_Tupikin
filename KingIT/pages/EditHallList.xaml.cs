using System;
using System.Collections.Generic;
using System.Data.Entity;
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

namespace KingIT.pages
{
    /// <summary>
    /// Логика взаимодействия для EditHallList.xaml
    /// </summary>
    public partial class EditHallList : Page
    {
        MainWindow main;
        halls currentHall;
        malls currentMall;

        public EditHallList(MainWindow _main, malls _currentMall)
        {
            InitializeComponent();
            main = _main;
            currentMall = _currentMall;
            FillStatusBox();
        }
        public EditHallList(MainWindow _main, halls _currentHall, malls _currentMall)
        {
            InitializeComponent();
            main = _main;
            currentHall = _currentHall;
            currentMall = _currentMall;
            FillStatusBox();
            FillBoxes();
        }
        private void FillStatusBox()
        {
            StatusBox.Items.Add("Арендован");
            StatusBox.Items.Add("Свободен");
            StatusBox.Items.Add("Забронирован");
        }
        private void FillBoxes()
        {
            FloorBox.Text = Convert.ToString(currentHall.floor);
            NumberBox.Text = currentHall.hallNum;
            AreaBox.Text = Convert.ToString(currentHall.area);
            FactorBox.Text = Convert.ToString(currentHall.valAddFactor);
            CostBox.Text = Convert.ToString(currentHall.cost);
            switch (currentHall.status)
            {
                case 5:
                    StatusBox.SelectedIndex = 1;
                    break;
                case 6:
                    StatusBox.SelectedIndex = 0;
                    break;
                case 7:
                    StatusBox.SelectedIndex = 2;
                    break;
            }
        }
        private void FillCurrent()
        {
            currentHall.floor = Convert.ToInt32(FloorBox.Text);
            currentHall.hallNum = NumberBox.Text;
            currentHall.area = Convert.ToDecimal(AreaBox.Text);
            currentHall.valAddFactor = Convert.ToDecimal(FactorBox.Text);
            currentHall.cost = Convert.ToDecimal(CostBox.Text);
            switch (StatusBox.SelectedIndex)
            {
                case 1:
                    currentHall.status = 5;
                    break;
                case 2:
                    currentHall.status = 6;
                    break;
                case 0:
                    currentHall.status = 7;
                    break;
            }
        }
        private void Exit(object sender, RoutedEventArgs args)
        {
            main.frame.Navigate(new HallsList(main, currentMall));
        }
        private void Save(object sender, RoutedEventArgs args)
        {
            try
            {
                using (var db = new KingITDBEntities1(main.connectionName))
                {
                    if (currentHall == null) //Если добавление
                    {
                        currentHall = new halls(); //новый павильон
                        currentHall.idHall = (from h in db.halls
                                               orderby h.idHall descending
                                               select h.idHall).FirstOrDefault();
                        currentHall.idHall += 1;
                        currentHall.idMall = currentMall.idMall;
                        FillCurrent(); //заполненый павильон
                        var selected = (from h in db.halls
                                        where h.idHall == currentHall.idHall
                                        select h).FirstOrDefault();
                        if (selected == null) //павильон не повторяется
                        {
                            db.halls.Add(currentHall);
                            db.SaveChanges();
                        }
                        else
                        {
                            MessageBox.Show($"{selected.idHall} {selected.hallNum} /// {currentHall.idHall} {currentHall.hallNum}");
                            throw new Exception("Уже есть такой павильон");
                        }
                    }
                    else
                    {
                        FillCurrent();
                        var selected = (from h in db.halls
                                        where h.idHall == currentHall.idHall
                                        select h).FirstOrDefault();
                        selected.hallNum = currentHall.hallNum;
                        selected.area = currentHall.area;
                        selected.status = currentHall.status;
                        selected.floor = currentHall.floor;
                        selected.valAddFactor = currentHall.valAddFactor;
                        selected.cost = currentHall.cost;
                        db.SaveChanges();
                    }

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            main.frame.Navigate(new HallsList(main, currentMall));
        }
    }
}