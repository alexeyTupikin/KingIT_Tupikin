using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media.Imaging;
using System.Windows.Input;
using System.Windows.Media;
using System.IO;
using System.Windows.Shapes;
using Microsoft.Win32;

namespace KingIT.pages
{
    /// <summary>
    /// Логика взаимодействия для addNewMall.xaml
    /// </summary>
    public partial class addNewMall : Window
    {
        public string fullPath = "";
        public string w_form = "";
        public forMalls current_item;
        public addNewMall(string form_)
        {
            InitializeComponent();
            w_form = form_;
            combo_status.Items.Add("План");
            combo_status.Items.Add("Реализация");
            combo_status.Items.Add("Строительство");
        }

        public addNewMall(forMalls sel_item, MainWindow _mainWindow, string _form)
        {
            InitializeComponent();
            w_form = _form;
            combo_status.Items.Add("План");
            combo_status.Items.Add("Реализация");
            combo_status.Items.Add("Строительство");
            current_item = sel_item;
            MainWindow mainWindow = _mainWindow;
            title_text.Text = current_item.title;
            combo_status.SelectedValue = current_item.StatusMall;
            qty_halls_text.Text = Convert.ToString(current_item.hallsCount);
            city_text.Text = current_item.address;
            cost_text.Text = Convert.ToString(current_item.cost);
            qty_floors_text.Text = Convert.ToString(current_item.floorsCount);
            kf_dop_cost_text.Text = Convert.ToString(current_item.valAddedFactor);
            if(current_item.icon != null)

            {
                image_view.Source = get_set_img.BytesToImage(current_item.icon);
                fullPath = Convert.ToString(get_set_img.BytesToImage(current_item.icon));
            }
        }
        //////////
        public void saveEditMall(forMalls select_item)
        {
            forMalls cur_mall = select_item;
            using (KingITDBEntities cnt = new KingITDBEntities())
            {
                malls edit_mall = (from m in cnt.malls where m.idMall == cur_mall.idMall select m).FirstOrDefault();
                if ((title_text.Text.Trim(' ') != "") && (combo_status.SelectedValue != null) &&
                (qty_halls_text.Text.Trim(' ') != "") && (city_text.Text.Trim(' ') != "") &&
                (cost_text.Text.Trim(' ') != "") && (qty_floors_text.Text.Trim(' ') != "") &&
                (kf_dop_cost_text.Text.Trim(' ') != "") && (fullPath != ""))
                {
                    edit_mall.title = title_text.Text;
                    switch (combo_status.SelectedIndex)
                    {
                        case 0:
                            {
                                edit_mall.idStatus = 1;
                                break;
                            }
                        case 1:
                            {
                                edit_mall.idStatus = 4;
                                break;
                            }
                        case 2:
                            {
                                edit_mall.idStatus = 2;
                                break;
                            }
                    }
                    edit_mall.hallsCount = Convert.ToInt32(qty_halls_text.Text);
                    edit_mall.address = city_text.Text;
                    edit_mall.cost = Convert.ToDecimal(cost_text.Text);
                    edit_mall.floorsCount = Convert.ToInt32(qty_floors_text.Text);
                    edit_mall.valAddedFactor = Convert.ToDecimal(kf_dop_cost_text.Text);
                    edit_mall.icon = get_set_img.GetImageBytes(fullPath);
                    cnt.SaveChanges();
                }
                else MessageBox.Show("Одно или несколько введенных вами значений не корректны. Сохранение отменено.");
            }
        }
        ///////

        public void saveNewMall()
        {
            malls new_mall = new malls();
            using (KingITDBEntities cnt = new KingITDBEntities())
            {
                new_mall.idMall = ((from m in cnt.malls select m.idMall).Max()) + 1;
                new_mall.title = title_text.Text;
                switch (combo_status.SelectedIndex)
                {
                    case 0:
                        {
                            new_mall.idStatus = 1;
                            break;
                        }
                    case 1:
                        {
                            new_mall.idStatus = 4;
                            break;
                        }
                    case 2:
                        {
                            new_mall.idStatus = 2;
                            break;
                        }
                }
                new_mall.hallsCount = Convert.ToInt32(qty_halls_text.Text);
                new_mall.address = city_text.Text;
                new_mall.cost = Convert.ToDecimal(cost_text.Text);
                new_mall.floorsCount = Convert.ToInt32(qty_floors_text.Text);
                new_mall.valAddedFactor = Convert.ToDecimal(kf_dop_cost_text.Text);
                new_mall.icon = get_set_img.GetImageBytes(fullPath);
                cnt.malls.Add(new_mall);
                cnt.SaveChanges();
            }
        }

        private void image_button_Click(object sender, RoutedEventArgs e)
        {
            var fileStream = new OpenFileDialog();
            fileStream.ShowDialog();
            fullPath = fileStream.FileName;
            byte[] image_byte = get_set_img.GetImageBytes(fullPath);
            image_view.Source = get_set_img.BytesToImage(image_byte);
        }

        private void save_button_Click(object sender, RoutedEventArgs e)
        {
            if ((title_text.Text.Trim(' ') != "") && (combo_status.SelectedValue != null) &&
                (qty_halls_text.Text.Trim(' ') != "") && (city_text.Text.Trim(' ') != "") &&
                (cost_text.Text.Trim(' ') != "") && (qty_floors_text.Text.Trim(' ') != "") &&
                (kf_dop_cost_text.Text.Trim(' ') != "") && (fullPath != ""))
            {
                try
                {
                    if (w_form == "add")
                    {
                        saveNewMall();
                    }
                    else if (w_form == "edit")
                    {
                        saveEditMall(current_item);
                    }
                    MessageBox.Show("Успешно сохранено");
                    this.Close();
                }
                catch
                {
                    MessageBox.Show("Сохранение не удалось.");
                }
            }
            else
            {
                MessageBox.Show("Одно или несколько значений не были введены. Сохранение отменено.");
            }
        }

        private void back_button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
