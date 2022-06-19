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

namespace KingIT.pages
{
    /// <summary>
    /// Логика взаимодействия для login.xaml
    /// </summary>
    public partial class login : Page
    {
        public int tryAcc = 0;
        public login()
        {
            InitializeComponent();
        }

        private void login_ac_Click(object sender, RoutedEventArgs e)
        {
            using (KingITDBEntities cnt = new KingITDBEntities())
            {
                var check_acc = cnt.login_proc(login_text.Text, pass_text.Password).FirstOrDefault();
                if (check_acc != null)
                {
                    //реализация перехода на нужную форму исходя их id и роли пользователя
                    //MessageBox.Show(Convert.ToString(check_acc)); Вывод id сотрудника
                    tryAcc = 0;
                }
                else
                {
                    tryAcc += 1;
                    if (tryAcc >= 3)
                    {
                        Capcha capchaWindow = new Capcha();
                        capchaWindow.ShowDialog();
                    }
                    else MessageBox.Show("Введенный вами логин или пароль неверен");
                }
            }
        }
    }
}
