using GreatBritain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using GreatBritain.Pages;

namespace GreatBritain
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Navigate(new CatalogOfGoodsPage());
            Manager.MainFrame = MainFrame;
        }
        private void WindowClosed(object sender, EventArgs e)
        {
            Owner.Show();
        }
        private void WindowClosing(object sender, CancelEventArgs e)
        {
            MessageBoxResult x = MessageBox.Show("Вы действительно хотите выйти?", "Выйти", MessageBoxButton.OKCancel, MessageBoxImage.Question);
            if (x == MessageBoxResult.Cancel) { e.Cancel = true; }
        }
        private void btnBackClick(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.GoBack();
        }
        private void btnEditGoodsClick(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new GoodsPage());
        }
        private void MainFrameContentRendered(object sender, EventArgs e)
        {
            if (MainFrame.CanGoBack)
            {
                btnBack.Visibility = Visibility.Visible;
                btnEditGoods.Visibility = Visibility.Collapsed;
            }
            else
            {
                btnBack.Visibility = Visibility.Collapsed;
                btnEditGoods.Visibility = Visibility.Visible;
            }
        }
    }
}
