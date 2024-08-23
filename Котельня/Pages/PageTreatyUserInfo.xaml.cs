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
    /// Логика взаимодействия для PageTreatyUserInfo.xaml
    /// </summary>
    public partial class PageTreatyUserInfo : Page
    {
        public PageTreatyUserInfo()
        {
            InitializeComponent();
            var userobj = KursavayaEntities.GetContext().Users.FirstOrDefault(x => x.id.ToString() == AppString.id);
            var Treatyobj = KursavayaEntities.GetContext().Treaty.FirstOrDefault(x => x.User_id == userobj.id);
            var Treatyprepaymentobj = KursavayaEntities.GetContext().Treaty_prepayment.FirstOrDefault(x => x.id == Treatyobj.Prepayment_id);
            fio.Text = userobj.FIO;
            C_.Text = Treatyprepaymentobj.description;
            date.Text = Treatyobj.Last_Date.ToString("dd-MM-yyyy");
        }
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.MainFrame.Navigate(new PageTreatyAdd(null));
        }
    }
}
