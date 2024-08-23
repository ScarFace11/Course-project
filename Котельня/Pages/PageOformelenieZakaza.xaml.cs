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
    /// Логика взаимодействия для PageOformelenieZakaza.xaml
    /// </summary>
    public partial class PageOformelenieZakaza : Page
    {
        private List<Item> selected;
        public PageOformelenieZakaza(List<Item> selectedItems)
        {
            InitializeComponent();

            selected = selectedItems;

            var userobj = KursavayaEntities.GetContext().Users.FirstOrDefault(x => x.id.ToString() == AppString.id.ToString());
            fiotext.Text = userobj.FIO;
            datatext.Text = thisDay.ToString("dd-MM-yyyy");
            sumtext.Text = AppString.PriceSum.ToString();
        }
        DateTime thisDay = DateTime.Today;
        private void btneditFirstDate(object sender, RoutedEventArgs e)
        {
            if (calendarPopupFirst.IsOpen)
            {
                calendarPopupFirst.IsOpen = false;
            }
            else
            {
                calendarPopupFirst.IsOpen = true;
            }
        }
        private void Calendar_SelectedDateChangedFirstDate(object sender, SelectionChangedEventArgs e)
        {
            DateTime? selectedDate = ((Calendar)sender).SelectedDate;

            if (selectedDate.HasValue)
            {

                textdate.Text = selectedDate.Value.ToShortDateString();
            }
        }
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.MainFrame.GoBack();
        }
        private void btnOform_Click(object sender, RoutedEventArgs e)
        {
            if (textdate.Text == "выбрать Дату" || Convert.ToDateTime(textdate.Text) < thisDay)
            {
                MessageBox.Show("Выбирите действительную дату");
            }
            else if (MessageBox.Show("Вы уверены", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
                {

                Order orderObj = new Order()
                {

                    User_id = Convert.ToInt32(AppString.id),
                    Order_date = thisDay,
                    Arrival_date = DateTime.Parse(textdate.Text),
                    Price = (float)Convert.ToDouble(sumtext.Text)
                };
                

                try
                {
                    KursavayaEntities.GetContext().Order.Add(orderObj);
                    KursavayaEntities.GetContext().SaveChanges();
                    MessageBox.Show("Заказ оформлен!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    AppFrame.MainFrame.Navigate(new PageOrderUserInfo());

                    int newOrderId = orderObj.id;

                    addOrderList(newOrderId, selected);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, ToString());
                }
                

            }
        }


        private void addOrderList (int orderId, List<Item> selected)
        {
            try
            {
                foreach (var orderItem in selected)
                {
                    Order_composition newOrderComposition = new Order_composition
                    {
                        WorkType_id = orderItem.id,
                        Order_id = orderId // Используем переданный ID заказа для каждого элемента из selected
                    };
                    KursavayaEntities.GetContext().Order_composition.Add(newOrderComposition);
                }
                KursavayaEntities.GetContext().SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при добавлении заказов: " + ex.Message);
            }
            
        }
    }
}
