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
    /// Логика взаимодействия для PageOrderUserInfo.xaml
    /// </summary>
    public partial class PageOrderUserInfo : Page
    {
        public static Order SelectedOrder { get; set; }
        public PageOrderUserInfo()
        {
            InitializeComponent();

            var orderobj = KursavayaEntities.GetContext().Order.Where(x => x.User_id.ToString() == AppString.id).ToList();
            DtgOrdersUser.ItemsSource = orderobj;
        }
        private void btnList_Click(object sender, RoutedEventArgs e)
        {

            //SelectedOrder = (Order)DtgOrdersUser.SelectedItem;

            //NavigationService.Navigate(new PageOrdersListUser(null));

            AppFrame.MainFrame.Navigate(new PageOrdersListUser((sender as Button).DataContext as Order));

        }
    }
}
