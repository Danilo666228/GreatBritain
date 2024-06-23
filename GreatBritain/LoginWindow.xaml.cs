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
using System.Windows.Shapes;

namespace GreatBritain
{

    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }
        private void btnCancelClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void btnOkClick(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var context = new EnglishStoreEntities())
                {
                    List<User> users = context.Users.ToList();
                    User u = users.FirstOrDefault(c => c.Login == txbLogin.Text && c.Password == txbPassword.Password);
                    if (u != null)
                    {
                        MainWindow mainWindow = new MainWindow();
                        mainWindow.Owner = this;
                        this.Hide();
                        mainWindow.Show();
                    }
                    else
                    {
                        MessageBox.Show("Неверные данные", "Авторизация", MessageBoxButton.OK, MessageBoxImage.Error);
                        txbLogin.Clear();
                        txbPassword.Clear();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            MessageBoxResult x = MessageBox.Show("Вы действительно хотите выйти?", "Выйти", MessageBoxButton.OKCancel, MessageBoxImage.Question);
            if (x == MessageBoxResult.Cancel) { e.Cancel = true; }
        }
    }
}
