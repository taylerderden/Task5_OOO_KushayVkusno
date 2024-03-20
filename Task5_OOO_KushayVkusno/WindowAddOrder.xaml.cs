using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
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
using Task5_OOO_KushayVkusno.DbModel;

namespace Task5_OOO_KushayVkusno
{
    /// <summary>
    /// Логика взаимодействия для WindowAddOrder.xaml
    /// </summary>
    public partial class WindowAddOrder : Window
    {
        Order order;
        int dishID, userID;
        public WindowAddOrder(Order order, int IdDish, int UserId)
        {
            InitializeComponent();
            dishID = IdDish; userID = UserId;   
            this.order = order;
            DataContext = order;

            List<Status> statuses = CoreModel.init().Statuses.ToList();
            cbStatus.ItemsSource = statuses;

            Dish dishes = CoreModel.init().Dishes.FirstOrDefault(p => Convert.ToString(p.IdDish).Contains(Convert.ToString(dishID)));
            if (dishes != null)
            {
                tblockDish.Text = dishes.DishName;
            }

        }

        private void cbStatus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Status statuses = CoreModel.init().Statuses.FirstOrDefault(p => p.StatusName.Contains(cbStatus.Text));
            if (statuses != null)
            {
                order.StatusIdStatus = statuses.IdStatus;
            }
        }

        private void Button_SaveClick(object sender, RoutedEventArgs e)
        {
            if (tbStol.Text != "" && tblockDish.Text != "" && cbStatus.Text != "")
            {
                order.UserIdUser = userID;
                DateOnly dateRec = new DateOnly(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                order.OrderDate = dateRec;
                order.OrderTime = DateTime.Now.ToString("HH:mm:ss");
                order.DishIdDish = dishID;

                CoreModel.init().Orders.Add(order);
                CoreModel.init().SaveChanges();

                MessageBox.Show("Запись добавлена");

                WindowOficiant windowOficiant = new WindowOficiant(userID);
                windowOficiant.Show();
                Hide();
            }
            else
                MessageBox.Show("Заполните все поля!");
        }
    }
}
