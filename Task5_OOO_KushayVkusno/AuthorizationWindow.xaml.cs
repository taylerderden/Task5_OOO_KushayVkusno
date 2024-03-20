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
using Task5_OOO_KushayVkusno.DbModel;

namespace Task5_OOO_KushayVkusno
{
    public partial class AuthorizationWindow : Window
    {
        public AuthorizationWindow()
        {
            InitializeComponent();
        }

        private void BtnAuthorization_Click(object sender, RoutedEventArgs e)
        {
            if (tbLogin.Text == null || tbPassword.Text == null)
            {
                MessageBox.Show("Заполните данные для входа!");
            }
            else
            {
                User user = CoreModel.init().Users.FirstOrDefault(p => p.UserLogin == tbLogin.Text && p.UserPassword == tbPassword.Text);

                if (user.RoleIdRole == 1)
                {
                    WindowOficiant windowOficiant = new WindowOficiant(user.IdUser);
                    windowOficiant.Show();
                    Hide();
                }
                else
                {
                    if (user.RoleIdRole == 2)
                    {
                        WindowAdmin windowAdmin = new WindowAdmin();
                        windowAdmin.Show();
                        Hide();
                    }
                }
            }
        }
    }
}
