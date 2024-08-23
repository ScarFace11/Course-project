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
    /// Логика взаимодействия для PageAllOrders.xaml
    /// </summary>
    public partial class PageAllOrders : Page
    {
        public static Order SelectedOrder { get; set; }
        public PageAllOrders()
        {
            InitializeComponent();
        }
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var OrderForRemoving = DtgOrders.SelectedItems.Cast<Order>().ToList();
            if (MessageBox.Show($"Вы точно хотите удалить следующие {OrderForRemoving.Count()} Элементов?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    KursavayaEntities.GetContext().Order.RemoveRange(OrderForRemoving);
                    KursavayaEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены");
                    DtgOrders.ItemsSource = KursavayaEntities.GetContext().Order.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }
        private void btnList_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.MainFrame.Navigate(new PageOrdersListUser((sender as Button).DataContext as Order));
        }
        private void btnFind_Click(object sender, RoutedEventArgs e)
        {
            //var find = KursavayaEntities.GetContext().Order.ToList();
            if (txtfindfio.Text != null)
            {
                var us = KursavayaEntities.GetContext().Users.FirstOrDefault(x => x.FIO == txtfindfio.Text);
                if (us != null)
                {
                    var find = KursavayaEntities.GetContext().Order.Where(x => x.User_id == us.id).ToList();
                    DtgOrders.ItemsSource = find;
                }
            }

            
        }
        private void btnSbross_Click(object sender, RoutedEventArgs e)
        {
            DtgOrders.ItemsSource = KursavayaEntities.GetContext().Order.ToList();
        }
        private void Page_IsVisibleChange(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                KursavayaEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                DtgOrders.ItemsSource = KursavayaEntities.GetContext().Order.ToList();
            }
        }
    }
}
