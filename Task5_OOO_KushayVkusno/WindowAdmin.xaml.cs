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
    /// <summary>
    /// Логика взаимодействия для WindowAdmin.xaml
    /// </summary>
    public partial class WindowAdmin : Window
    {
        public WindowAdmin()
        {
            InitializeComponent();

            IEnumerable<Dish> dishes = CoreModel.init().Dishes
                .Include(p => p.CategoryIdCategoryNavigation);
            lvDish.ItemsSource = dishes.ToList();

            cbSort.Items.Add("По возрастанию");
            cbSort.Items.Add("По убыванию");
            cbSort.SelectedIndex = 0;

            List<Category> categories = CoreModel.init().Categories.ToList();
            cbFiltr.ItemsSource = categories;
            categories.Insert(0, new Category { CategoryName = "Все категории" });
            cbFiltr.SelectedIndex = 0;
        }

        private void btnDishAdd_Click(object sender, RoutedEventArgs e)
        {
            WindowAddDish add_Window = new WindowAddDish(new Dish());
            add_Window.Show();
            this.Close();
        }
        private void update()
        {
            IEnumerable<Dish> dishes = CoreModel.init().Dishes
                 .Include(p => p.CategoryIdCategoryNavigation)
                 .Where(p => p.DishName.Contains(tbSearch.Text)
                 || Convert.ToString(p.DishPrice).Contains(tbSearch.Text)
                 || p.DishSostav.Contains(tbSearch.Text));


            switch (cbSort.SelectedIndex)
            {
                case 0:
                    dishes = dishes.OrderBy(p => p.DishPrice);
                    break;

                case 1:
                    dishes = dishes.OrderByDescending(p => p.DishPrice);
                    break;
            }
            if (cbFiltr.SelectedIndex > 0)
            {
                dishes = dishes.Where(p => p.CategoryIdCategory == (cbFiltr.SelectedItem as Category).IdCategory);
            }
            lvDish.ItemsSource = dishes.ToList();
        }

        private void tbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            update();
        }

        private void cbSortChanged(object sender, SelectionChangedEventArgs e)
        {
            update();
        }

        private void cbFiltrChanged(object sender, SelectionChangedEventArgs e)
        {
            update();
        }

        private void btnOrders_Click(object sender, RoutedEventArgs e)
        {
            WindowOrders windowOrders = new WindowOrders();
            windowOrders.Show();
        }

        private void Button_DelClick(object sender, RoutedEventArgs e)
        {
            if (lvDish.SelectedItems.Count > 1 || lvDish.SelectedItems.Count < 1)
            {
                MessageBox.Show("выберите 1 элемент");
                return;
            }

            Dish dishDel = lvDish.SelectedItem as Dish;

            if (dishDel != null && MessageBox.Show("Delete?", "Realy delete?", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Order order;
                CoreModel.init().Dishes.Remove(dishDel);
                CoreModel.init().Orders.RemoveRange(CoreModel.init().Orders.Where(p => Convert.ToString(p.DishIdDish).Contains(Convert.ToString(dishDel.IdDish))));
                CoreModel.init().SaveChanges();
                update();
            }
        }

        private void Button_UpdateClick(object sender, RoutedEventArgs e)
        {
            Dish dishEdit = lvDish.SelectedItem as Dish;
            WindowAddDish add_Window = new WindowAddDish(dishEdit);
            add_Window.Show();
            this.Close();
        }
    }
}
