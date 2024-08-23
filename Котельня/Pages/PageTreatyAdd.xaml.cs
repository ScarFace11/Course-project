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
    /// Логика взаимодействия для PageTreatyAdd.xaml
    /// </summary>
    public partial class PageTreatyAdd : Page
    {
        public Treaty _currentTreaty = new Treaty();
        public PageTreatyAdd(Treaty selectedTreaty)
        {
            InitializeComponent();
            if (selectedTreaty != null)
                _currentTreaty = selectedTreaty;
            DataContext = _currentTreaty;

            cmbpay.ItemsSource = KursavayaEntities.GetContext().Treaty_prepayment.ToList();
            
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
        private void btnEditLastDate(object sender, RoutedEventArgs e)
        {
            if (calendarPopupLast.IsOpen)
            {
                calendarPopupLast.IsOpen = false;
            }
            else
            {
                calendarPopupLast.IsOpen = true;
            }
        }

        private void Calendar_SelectedDateChangedFirstDate(object sender, SelectionChangedEventArgs e)
        {
            DateTime? selectedDate = ((Calendar)sender).SelectedDate;

            if (selectedDate.HasValue)
            {
                _currentTreaty.First_Date = selectedDate.Value;
                FDate.Text = selectedDate.Value.ToShortDateString();
            }
        }
        private void Calendar_SelectedDateChangedLastDate(object sender, SelectionChangedEventArgs e)
        {
            DateTime? selectedDate = ((Calendar)sender).SelectedDate;

            if (selectedDate.HasValue)
            {

                _currentTreaty.Last_Date = selectedDate.Value;
                LDate.Text = selectedDate.Value.ToShortDateString();
            }
        }
        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            
            if (Visibility == Visibility.Visible)
            {
                if (AppString.Role_name == "Админ")
                {
                    fio.IsReadOnly = false;
                    btnFdate.Visibility = Visibility.Visible;
                    btnLdate.Visibility = Visibility.Visible;
                }
                else
                {
                    fio.IsReadOnly = true;
                    btnFdate.Visibility = Visibility.Collapsed;
                    btnLdate.Visibility = Visibility.Collapsed;
                }
                
                var User_id = KursavayaEntities.GetContext().Users.FirstOrDefault(x => x.id == _currentTreaty.User_id);
                if (User_id != null)
                {
                    _currentTreaty.User_id = User_id.id;
                    fio.Text = User_id.FIO;
                    FDate.Text = _currentTreaty.First_Date.ToString("dd/MM/yyyy");
                    LDate.Text = _currentTreaty.Last_Date.ToString("dd/MM/yyyy");
                }
                else
                {
                    if(AppString.Role_name != "Админ")
                    {
                        User_id = KursavayaEntities.GetContext().Users.FirstOrDefault(x => x.id.ToString() == AppString.id.ToString());
                        _currentTreaty.User_id = User_id.id;
                        fio.Text = User_id.FIO;
                    }

                    FDate.Text = thisDay.ToString("dd/MM/yyyy");
                    LDate.Text = thisDay.AddYears(1).ToString("dd/MM/yyyy");

                    

                }
                _currentTreaty.First_Date = Convert.ToDateTime(FDate.Text);
                _currentTreaty.Last_Date = Convert.ToDateTime(LDate.Text);
            }
        }

        private void btncreate(object sender, RoutedEventArgs e)
        {
            
            if (cmbpay.Text.Length < 1)
            {
                MessageBox.Show("Выбирите предоплату!");
            }
            else if (Convert.ToDateTime(FDate.Text) > Convert.ToDateTime(LDate.Text))
            {
                MessageBox.Show("Дата окончания должа быть больше, чем дата начала");
            }
            else
            {
                if (MessageBox.Show("Вы уверены", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
                {                   
                    var User_id = KursavayaEntities.GetContext().Users.FirstOrDefault(x => x.id == _currentTreaty.User_id);

                    var Treatyobj = KursavayaEntities.GetContext().Treaty.FirstOrDefault(x => x.id == _currentTreaty.id);
                    
                    if (User_id == null)
                    {
                        User_id = KursavayaEntities.GetContext().Users.FirstOrDefault(x => x.FIO == fio.Text);
                        if (User_id == null)
                        {
                            MessageBox.Show("Неправильно введено фио пользователя");
                            return;
                        }

                        int id_value;
                        var payObj = KursavayaEntities.GetContext().Treaty_prepayment.FirstOrDefault(x => x.value.ToString() == cmbpay.Text);

                        id_value = payObj.id;
                        Treaty treatyObj = new Treaty()
                        {

                            User_id = User_id.id,
                            First_Date = DateTime.Parse(FDate.Text),
                            Last_Date = DateTime.Parse(LDate.Text),
                            Prepayment_id = id_value
                        };
                        KursavayaEntities.GetContext().Treaty.Add(treatyObj);
                        KursavayaEntities.GetContext().SaveChanges();
                        MessageBox.Show("Данные успешно добавлены!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);

                    }
                    else
                    {

                        var Treaty_user_id = KursavayaEntities.GetContext().Treaty.FirstOrDefault(x => x.User_id == User_id.id);

                        if (_currentTreaty != Treatyobj && Treaty_user_id == null)
                        {
                            KursavayaEntities.GetContext().Treaty.Add(_currentTreaty);
                            MessageBox.Show("Данные клиента успешно добавлены!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        else
                        {
                            int id_value;
                            var payObj = KursavayaEntities.GetContext().Treaty_prepayment.FirstOrDefault(x => x.value.ToString() == cmbpay.Text);
                            id_value = payObj.id;

                            Treaty_user_id.First_Date = Convert.ToDateTime(FDate.Text);
                            Treaty_user_id.Last_Date = Convert.ToDateTime(LDate.Text);
                            Treaty_user_id.Prepayment_id = id_value;

                            MessageBox.Show("Данные изменены!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                    }
                    
                   
                    try
                    {
                        KursavayaEntities.GetContext().SaveChanges();
                        if (AppString.Role_name == "Админ")
                        {
                            AppFrame.MainFrame.GoBack();
                        }
                        else
                            AppFrame.MainFrame.Navigate(new Profile());
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, ToString());
                    }
                }                   
            } 
        }
    }
}
