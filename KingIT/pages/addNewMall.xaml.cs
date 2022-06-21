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
        public addNewMall()
        {

            InitializeComponent();
            combo_status.Items.Add("План");
            combo_status.Items.Add("Реализация");
            combo_status.Items.Add("Строительство");
        }

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
                    saveNewMall();
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
