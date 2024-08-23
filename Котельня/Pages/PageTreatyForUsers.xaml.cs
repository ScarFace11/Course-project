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
    /// Логика взаимодействия для PageTreatyForUsers.xaml
    /// </summary>
    public partial class PageTreatyForUsers : Page
    {
        public PageTreatyForUsers()
        {
            InitializeComponent();
        }
        private void chkbox_Checked(object sender, RoutedEventArgs e)
        {
            btnNext.IsEnabled = true;
        }
        private void chkbox_Unchecked(object sender, RoutedEventArgs e)
        {
            btnNext.IsEnabled = false;
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.MainFrame.Navigate(new PageTreatyAdd(null));
        }
    }
}
