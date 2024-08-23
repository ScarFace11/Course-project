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
using Котельня.Pages;
using Котельня.ApplicationData;

namespace Котельня
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            AppFrame.MainFrame = frmain;
            AppFrame.MainFrame.Navigate(new MainMenu());
            AppString.Role_name = "Гость"; //--
        }
        DateTime thisDay = DateTime.Today;
        private void frmain_Navigating(object sender, NavigatingCancelEventArgs e)
        {
            string role = AppString.Role_name;
            if (role != null)
                lblnameROle.Text = role;

            if (role == "Гость")
            {
                SP_Order.Visibility = Visibility.Collapsed;
                SP_Treaty.Visibility = Visibility.Collapsed;
                SP_Clients.Visibility = Visibility.Collapsed;
            }
            else if (role == "Клиент")
            {
                SP_Order.Visibility = Visibility.Visible;
                SP_Treaty.Visibility = Visibility.Visible;
            }

            else if (role == "Админ")
            {
                SP_Order.Visibility = Visibility.Visible;
                SP_Treaty.Visibility = Visibility.Visible;
                SP_Clients.Visibility = Visibility.Visible;
            }
        }

        private void MainFrame_Navigated(object sender, NavigationEventArgs e)
        {
            if ((e.Content is PageTypeWork || e.Content is PageWork) && AppString.Role_name == "Админ")
            {
                btnCategoryWork.Visibility = Visibility.Visible;
                btnTypeWork.Visibility = Visibility.Visible;
            }
            else
            {
                btnCategoryWork.Visibility = Visibility.Collapsed;
                btnTypeWork.Visibility = Visibility.Collapsed;          
            }

            if ((e.Content is PageAddRole || e.Content is PageAllUsers || e.Content is PageRoles || e.Content is PageAllUsers) && AppString.Role_name == "Админ")
            {
                btnRoleselect.Visibility = Visibility.Visible;
                btnUserselect.Visibility = Visibility.Visible;
            }
            else
            {
                btnRoleselect.Visibility = Visibility.Collapsed;
                btnUserselect.Visibility = Visibility.Collapsed;
            }

            if ((e.Content is PageTeatys || e.Content is PageTreatyAdd || e.Content is PageTreatyPrepaymentList) && AppString.Role_name == "Админ")
            {
                btnAllTreaty.Visibility = Visibility.Visible;
                btnTreatyPrepayment.Visibility = Visibility.Visible;
            }
            else
            {
                btnAllTreaty.Visibility = Visibility.Collapsed;
                btnTreatyPrepayment.Visibility = Visibility.Collapsed;
            }
        }
        private void aPicture_MouseDown(object sender, MouseEventArgs e)
        {
            if (AppString.Role_name != "Гость")
            {
                AppFrame.MainFrame.Navigate(new Profile());
            }
            else AppFrame.MainFrame.Navigate(new PageLogin());
        }
        private void Order_MouseDown(object sender, MouseEventArgs e)
        {
            if (AppString.Role_name == "Админ")
            {
                AppFrame.MainFrame.Navigate(new PageAllOrders());
            }
            else if (AppString.Role_name == "Клиент")
            {
                AppFrame.MainFrame.Navigate(new PageOrderUserInfo());
            }
        }
        private void TypeWorkPicture_MouseDown(object sender, MouseEventArgs e)
        {
            AppFrame.MainFrame.Navigate(new PageTypeWork()); 
        }
        private void UsersPicture_MouseDown(object sender, RoutedEventArgs e)
        {
            AppFrame.MainFrame.Navigate(new PageAllUsers());
        }
        private void FAQPicture_MouseDown(object sender, RoutedEventArgs e)
        {
            AppFrame.MainFrame.Navigate(new MainMenu());
        }
        private void TreatyPicture_MouseDown(object sender, RoutedEventArgs e)
        {

            
            if (AppString.Role_name == "Админ")
            {
                AppFrame.MainFrame.Navigate(new PageTeatys());
            }
            else
            {
                var userTreaty = KursavayaEntities.GetContext().Treaty.FirstOrDefault(x => x.User_id.ToString() == AppString.id);
                if (userTreaty != null &&  userTreaty.Last_Date > thisDay)
                {
                    AppFrame.MainFrame.Navigate(new PageTreatyUserInfo());
                }
                else
                {
                    AppFrame.MainFrame.Navigate(new PageTreatyForUsers());
                }
                
            }     
        }
        
        private void Btn_Work_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.MainFrame.Navigate(new PageTypeWork());
        }
        private void Btn_WorkCategory_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.MainFrame.Navigate(new PageWork());
        }
        private void Btn_User_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.MainFrame.Navigate(new PageAllUsers());
        }
        private void Btn_Role_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.MainFrame.Navigate(new PageRoles());
        }

        private void Btn_Treaty_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.MainFrame.Navigate(new PageTeatys());
        }
        private void Btn_Treaty_Prepayment_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.MainFrame.Navigate(new PageTreatyPrepaymentList());
        }


        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                
            }
        }

        
    }
}
