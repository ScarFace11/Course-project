using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Логика взаимодействия для PageOrdersListUser.xaml
    /// </summary>
    public class Items2
    {
        public string WorkTypeid { get; set; }
    }
    public partial class PageOrdersListUser : Page
    {
        private ObservableCollection<Item> items2 = new ObservableCollection<Item>();

        public Order_composition _currentTreaty = new Order_composition();
        public Order _currentOrder = new Order();
        public PageOrdersListUser(Order selectedTreaty)
        {
            InitializeComponent();
            if (selectedTreaty != null)
                _currentOrder = selectedTreaty;
            DataContext = _currentOrder;
        }
        private void btn_Back_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.MainFrame.GoBack();
        }
        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

            using (var context = new KursavayaEntities())
            {
                items2 = new ObservableCollection<Item>((IEnumerable<Item>)context.Order_composition
                    .Where(x => x.Order_id == _currentOrder.id)
                    .Select(w => new Item
                    {
                        WorkTypeid = w.WorkType_id.ToString()
                    }).ToList());
            }         
            foreach (var WT in items2)
            {
                var cc = KursavayaEntities.GetContext().WorkType.FirstOrDefault(x => x.id.ToString() == WT.WorkTypeid);
                WT.WorkTypeid = cc?.name;
            }
            DtgOrdersList.ItemsSource = items2;
        }
    }
}
