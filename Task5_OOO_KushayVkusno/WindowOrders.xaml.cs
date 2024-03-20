using Microsoft.EntityFrameworkCore;
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
using Task5_OOO_KushayVkusno.DbModel;

namespace Task5_OOO_KushayVkusno
{
    public partial class WindowOrders : Window
    {
        public WindowOrders()
        {
            InitializeComponent();

            IEnumerable<Order> orders = CoreModel.init().Orders
                 .Include(p => p.DishIdDishNavigation)
                 .Include(p => p.StatusIdStatusNavigation)
                 .Include(p => p.UserIdUserNavigation);
            ListViewOrder.ItemsSource = orders.ToList();

        }
    }
}
