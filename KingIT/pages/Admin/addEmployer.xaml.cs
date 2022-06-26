using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.IO;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using KingIT.Model;
using Microsoft.Win32;

namespace KingIT.pages.Admin
{
    /// <summary>
    /// Логика взаимодействия для addEmployer.xaml
    /// </summary>
    public partial class addEmployer : Window
    {
        public MainWindow main;
        public string fullPath = "";
        public string w_form;
        employers currentEmp;
        public addEmployer(MainWindow _main, string _w_form)
        {
            InitializeComponent();
            main = _main;
            w_form = _w_form;
            fillCBsex();
            fillCBPosition();
        }

        public addEmployer(MainWindow _main, string _w_form, employers cur_emp)
        {
            InitializeComponent();
            main = _main;
            currentEmp = cur_emp;
            lastname_text.Text = currentEmp.surname;
            name_text.Text = currentEmp.name;
            middlename_text.Text = currentEmp.middlename;
            login_text.Text = currentEmp.login;
            password_text.Text = currentEmp.password;
            sex_cb.SelectedValue = currentEmp.sex;
            position_cb.SelectedIndex = currentEmp.idPost - 1;
            phone_text.Text = currentEmp.phone;
            image_view.Source = get_set_img.BytesToImage(currentEmp.photo);
            w_form = _w_form;
            fillCBsex();
            fillCBPosition();
        }

        public void fillCBsex()
        {
            using (KingITDBEntities1 cnt = new KingITDBEntities1(main.connectionName))
            {
                sex_cb.ItemsSource = (from s in cnt.employers select s.sex).Distinct().ToList();
            }
        }

        public void fillCBPosition()
        {
            using (KingITDBEntities1 cnt = new KingITDBEntities1(main.connectionName))
            {
                position_cb.ItemsSource = (from p in cnt.postes where p.title != "Удален" select p.title).ToList();
            }
        }

        private void save_but_Click(object sender, RoutedEventArgs e)
        {
            if (
                (lastname_text.Text.Trim(' ') != "") && (name_text.Text.Trim(' ') != "") &&
                (middlename_text.Text.Trim(' ') != "") && (login_text.Text.Trim(' ') != "") &&
                (password_text.Text.Trim(' ') != "") && (sex_cb.SelectedValue != null) &&
                (position_cb.SelectedValue != null) && (phone_text.Text.Trim(' ') != "") &&
                (fullPath != "")
                )
            {
                using (KingITDBEntities1 cnt = new KingITDBEntities1(main.connectionName))
                {
                    employers new_emp = new employers();
                    if (w_form == "edit")
                    {
                        new_emp = (from _emp in cnt.employers where _emp.idEmployer == currentEmp.idEmployer select _emp).FirstOrDefault();
                    }
                    new_emp.surname = lastname_text.Text;
                    new_emp.name = name_text.Text;
                    new_emp.middlename = middlename_text.Text;
                    new_emp.login = login_text.Text;
                    new_emp.password = password_text.Text;
                    new_emp.sex = sex_cb.SelectedValue.ToString();
                    new_emp.idPost = (from p in cnt.postes where p.title == position_cb.SelectedValue.ToString() select p.idPost).FirstOrDefault();
                    new_emp.phone = phone_text.Text;
                    if (new_emp.photo == null)
                    {
                        new_emp.photo = get_set_img.GetImageBytes(fullPath);
                    }
                    else
                    {
                        new_emp.photo = currentEmp.photo;
                    }
                    if (w_form == "add")
                    {
                        new_emp.idEmployer = (from emp in cnt.employers select emp.idEmployer).Max() + 1;
                        cnt.employers.Add(new_emp);
                    }
                    cnt.SaveChanges();
                }
            }
            else MessageBox.Show("Одно или несколько из введенных вами полей некорректно или имеет пустое значение.");
        }

        private void back_but_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void img_but_Click(object sender, RoutedEventArgs e)
        {
            var fileStream = new OpenFileDialog();
            fileStream.ShowDialog();
            fullPath = fileStream.FileName;
            byte[] image_byte = get_set_img.GetImageBytes(fullPath);
            image_view.Source = get_set_img.BytesToImage(image_byte);
        }
    }
}
