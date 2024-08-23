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
    /// Логика взаимодействия для PageLogin.xaml
    /// </summary>
    public partial class PageLogin : Page
    {
        public PageLogin()
        {
            InitializeComponent();
            
            
        }
        private void btnToComeIn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var userObj = KursavayaEntities.GetContext().Users.FirstOrDefault(x => x.login == txbLogin.Text && x.password == psbPassword.Password);

                if (userObj == null)
                {
                    MessageBox.Show("Пользователь с таким логином и паролем не найден!", "Ошибка авторизации", MessageBoxButton.OK, MessageBoxImage.Error);

                }
                else
                {

                    var roleObj = KursavayaEntities.GetContext().Role.FirstOrDefault(x => x.id == userObj.Role_id);
                    AppString.id = userObj.id.ToString();
                    AppString.Role_name = roleObj.name;
                    AppFrame.MainFrame.Navigate(new Profile());
                    /*
                    else
                    {
                        AppFrame.MainFrame.Navigate(new PageAllUsers());
                    } */
                    
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show("Ошибка:" + Ex.Message.ToString(), "Критическая работа приложения", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

        }
        private void btnReg(object sender, RoutedEventArgs e)
        {
            AppFrame.MainFrame.Navigate(new PageCreateAccount());
        }
        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                {
                    txbLogin.Foreground = Brushes.Gray;
                    psbPassword.Foreground = Brushes.Gray;
                    AppString.Role_name = "Гость";
                }
            }
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txbLogin.Text == "Логин...")
            {
            txbLogin.Text = null;
            txbLogin.Foreground = Brushes.Black;
            }



        }

        private void psbPassword_GotFocus(object sender, RoutedEventArgs e)
        {
            if (psbPassword.Password == "Пароль")
            {
                psbPassword.Password = null;
                psbPassword.Foreground = Brushes.Black;
            }

        }
    }
}
