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

namespace KingIT.pages
{
    /// <summary>
    /// Логика взаимодействия для login.xaml
    /// </summary>
    public partial class login : Page
    {
        public int tryAcc = 0;
        MainWindow mainWindow;
        public login(MainWindow _mainWindow)
        {
            InitializeComponent();
            mainWindow.Title = "Авторизация";
            mainWindow = _mainWindow;
        }

        private void login_ac_Click(object sender, RoutedEventArgs e)
        {
            using (KingITDBEntities1 cnt = new KingITDBEntities1())
            {
                var check_acc = (from emp in cnt.employers where emp.login == login_text.Text && emp.password == pass_text.Password select emp.idEmployer).FirstOrDefault();
                if (check_acc != null)
                {
                    //реализация перехода на нужную форму исходя их id и роли пользователя
                    using (KingITDBEntities1 db = new KingITDBEntities1())
                    {
                        var buf_position = (from emp in db.employers where emp.idEmployer == check_acc select emp.idPost).FirstOrDefault();
                        switch (buf_position)
                        {
                            case 1: //Admin
                                {
                                    break;
                                }
                            case 2: //Manager A
                                {
                                    break;
                                }
                            case 3: //Manager C
                                {
                                    //
                                    MessageBox.Show("Ваш тип пользователя: Менеджер С");
                                    mainWindow.frame.Navigate(new pages.iManagerC(mainWindow));
                                    break;
                                }
                            case 4: //Delete
                                {
                                    MessageBox.Show("Пользователь удален.");
                                    break;
                                }
                        }
                    }
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
                        tryAcc = 0;
                        login_text.Text = "";
                        pass_text.Password = "";
                    }
                    else MessageBox.Show("Введенный вами логин или пароль неверен");
                }
            }
        }
    }
}
