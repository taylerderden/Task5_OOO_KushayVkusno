using Microsoft.EntityFrameworkCore;
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
    /// Логика взаимодействия для WindowOficiant.xaml
    /// </summary>
    public partial class WindowOficiant : Window
    {
        int UserId;
        public WindowOficiant(int IdUser)
        {
            UserId = IdUser;
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

        private void btnOrder_Click(object sender, RoutedEventArgs e)
        {
            Dish dishes = lvDish.SelectedItem as Dish;
            if (dishes != null)
            {
                WindowAddOrder window_AddOrder = new WindowAddOrder(new Order(), dishes.IdDish, UserId);
                window_AddOrder.Show();
                this.Close();
            }
            else
                MessageBox.Show("Выбери блюдо!");
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
    }
}
