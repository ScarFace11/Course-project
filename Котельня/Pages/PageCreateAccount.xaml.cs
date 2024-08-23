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
using Котельня.ApplicationData;

namespace Котельня.Pages
{
    /// <summary>
    /// Логика взаимодействия для PageCreateAccount.xaml
    /// </summary>
    public partial class PageCreateAccount : Page
    {
        public PageCreateAccount()
        {
            InitializeComponent();

        }
        bool admincheck = false;
        int kodnumber = 1111;
        private void chkbox_Checked(object sender, RoutedEventArgs e)
        {
            btnreg.IsEnabled = true;
        }
        private void chkbox_admin_Checked(object sender, RoutedEventArgs e)
        {
            SPKod.Visibility = Visibility.Visible;
            admincheck = true;
        }
        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (string.IsNullOrWhiteSpace(txbLogin.Text))
            {
                errors.AppendLine("Введите Логин");
            }
            if (string.IsNullOrWhiteSpace(psbPass1.Password))
            {
                errors.AppendLine("Введите Пароль");
            }
            if (string.IsNullOrWhiteSpace(psbPass2.Password))
            {
                errors.AppendLine("Повторно Введите Пароль");
            }
            if (admincheck && string.IsNullOrWhiteSpace(txbkod.Text))
            {
                errors.AppendLine("Введите Код");
            }
            if (admincheck && txbkod.Text != kodnumber.ToString())
            {
                errors.AppendLine("Неверный Код");
            }
            if (psbPass2.Password != psbPass1.Password || psbPass2.Password.Length < 1 || psbPass1.Password.Length < 1)
            {
                errors.AppendLine("Пароль неверен");
                psbPass1.Background = Brushes.LightCoral;
                psbPass1.BorderBrush = Brushes.Red;
                psbPass2.Background = Brushes.LightCoral;
                psbPass2.BorderBrush = Brushes.Red;
            }
            else
            {
                psbPass1.Background = Brushes.LightGreen;
                psbPass1.BorderBrush = Brushes.Green;
                psbPass2.Background = Brushes.LightGreen;
                psbPass2.BorderBrush = Brushes.Green;
            }
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            if (KursavayaEntities.GetContext().Users.Count(x => x.login == txbLogin.Text) > 0)
            {
                MessageBox.Show("Пользователь с таким логином уже существует!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            AppString.login = txbLogin.Text;
            AppString.password = psbPass1.Password;
            
            if (admincheck)
            {
                AppString.Role = "Админ";
            }
            else AppString.Role = "Клиент";
            
            AppFrame.MainFrame.Navigate(new PageCreateProfile(null));
        }

        private void chkbox_Unchecked(object sender, RoutedEventArgs e)
        {
            btnreg.IsEnabled = false;
        }
        private void chkbox_admin_Unchecked(object sender, RoutedEventArgs e)
        {
            SPKod.Visibility = Visibility.Collapsed;
            admincheck = false;
        }
    }
}
