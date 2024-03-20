using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
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
    /// Логика взаимодействия для WindowAddDish.xaml
    /// </summary>
    public partial class WindowAddDish : Window
    {
        Dish dish;
        public WindowAddDish(Dish dish)
        {
            InitializeComponent();
            this.dish = dish;
            DataContext= dish;
            List<Category> categories = CoreModel.init().Categories.ToList();
            cbCategory.ItemsSource = categories;
        }

        private void Button_SaveClick(object sender, RoutedEventArgs e)
        {
            if (dish.IdDish == 0)
            {
                CoreModel.init().Dishes.Add(dish);
            }

            CoreModel.init().SaveChanges();

            WindowAdmin window_Admin = new WindowAdmin();
            window_Admin.Show();
            this.Close();
        }

        private void imageEditClick(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog { Filter = "Jpeg files|*.jpg|All Files|*.*" };
            if (openFile.ShowDialog() == true)
            {
                dish.DishPhoto = File.ReadAllBytes(openFile.FileName);
                //imgPhoto.Source = new BitmapImage(new Uri(new FileInfo(openFile.FileName).Name, UriKind.Relative));
                imgPhoto.Source = new BitmapImage(new Uri(openFile.FileName, UriKind.Absolute));
            }
        }

        private void cbCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
