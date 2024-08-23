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
using Microsoft.Win32;
using System.IO;
using Path = System.IO.Path;

namespace Котельня.Pages
{
    /// <summary>
    /// Логика взаимодействия для Profile.xaml
    /// </summary>
    public partial class Profile : Page
    {

        public Profile()
        {
            InitializeComponent();
        }
        DateTime thisDay = DateTime.Today;
        
        private void btn_LogOut(object sender, RoutedEventArgs e)
        {
            AppString.Role_name = "Гость";
            AppFrame.MainFrame.Navigate(new PageLogin());
        }
        private void BtnTreatyoform_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.MainFrame.Navigate(new PageTreatyForUsers());
        }

        private void btn_profileEdit(object sender, RoutedEventArgs e)
        {
            var id = KursavayaEntities.GetContext().Users.FirstOrDefault(x => x.id.ToString() == AppString.id);
            AppFrame.MainFrame.Navigate(new PageCreateProfile(id));
        }
        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                
                
                var User_id = KursavayaEntities.GetContext().Users.FirstOrDefault(x => x.id.ToString() == AppString.id.ToString());
                var Role = KursavayaEntities.GetContext().Role.FirstOrDefault(x => x.id == User_id.Role_id);

                if (User_id.ImageName != null)
                {
                    string projectPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
                    string imagesFolderPath = Path.Combine(projectPath, "Images");

                    string imageFileName = User_id.ImageName;
                    string destinationImagePath = Path.Combine(imagesFolderPath, imageFileName);
                    ImageProfile.Source = new BitmapImage(new Uri(destinationImagePath));
                }
                
                //if (AppString.ImagePath != null)
                //{
                    //ImageProfile.Source = new BitmapImage(new Uri(AppString.ImagePath));
                //}
                

                idlb.Content = User_id.id;
                FIOlb.Content = User_id.FIO;              
                adrlb.Content = User_id.address;
                phonelb.Content = User_id.phone_number;

                AppString.Role_name = Role.name;

                if (AppString.Role_name == "Админ")
                {
                    TreatySP.Visibility = Visibility.Collapsed;
                }
                else
                {
                    var Treaty_id = KursavayaEntities.GetContext().Treaty.FirstOrDefault(x => x.User_id.ToString() == idlb.Content.ToString());

                    if (Treaty_id == null)
                    {
                        TreatyActivelbl.Content = "Не активен";
                        btnoform.Visibility = Visibility.Visible;
                    }
                    else if (thisDay.Date > Treaty_id.Last_Date.Date)
                    {
                        TreatyActivelbl.Content = "Просрочен";
                        btnoform.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        btnoform.Visibility = Visibility.Collapsed;
                        TreatyActivelbl.Content = "Активен до";
                        TreatyLastDatelbl.Visibility = Visibility.Visible;
                        TreatyLastDatelbl.Content = Treaty_id.Last_Date.ToString("dd/MM/yyyy");
                        SPTreatyPrep.Visibility = Visibility.Visible;

                        var prepayobj = KursavayaEntities.GetContext().Treaty_prepayment.FirstOrDefault(x => x.id == Treaty_id.Prepayment_id);

                        Treatyprepaymentlbl.Content = prepayobj.description;
                    }
                }
                
            }
        }

    }
}
