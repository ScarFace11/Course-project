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
using System.Collections.ObjectModel;

using System.ComponentModel;

namespace Котельня.Pages
{
    /// <summary>
    /// Логика взаимодействия для PageAddUserOrder.xaml
    /// </summary>
    /// // Пример ViewModel для страницы SecondPage

    public partial class PageAddUserOrder : Page
    {
        private List<Item> selected;
        public PageAddUserOrder(List<Item> selectedItems)
        {
            InitializeComponent();
            DGridAddOrder.ItemsSource = selectedItems;
            selected = selectedItems;
        }
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.MainFrame.GoBack();

        }
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            double totalSum = 0.0;

            foreach (Item item in DGridAddOrder.Items)
            {
                double priceValue;
                if (double.TryParse(item.Price.ToString(), out priceValue))
                {
                    totalSum += priceValue;
                }
            }
            var Treatyobj = KursavayaEntities.GetContext().Treaty.FirstOrDefault(x => x.User_id.ToString() == AppString.id);
            var prepay = KursavayaEntities.GetContext().Treaty_prepayment.FirstOrDefault(x => x.id == Treatyobj.Prepayment_id);

            double dis = totalSum * (prepay.C_ / 100.0);
            AppString.PriceSum = totalSum - dis;

            AppFrame.MainFrame.Navigate(new PageOformelenieZakaza(selected));
        }
        
    }
   
}
