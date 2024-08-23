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
    /// Логика взаимодействия для PageCreateProfile.xaml
    /// </summary>
    public partial class PageCreateProfile : Page
    {
        public Users _currentClient = new Users();
        public static string imgaefile { get; set; }
        public PageCreateProfile(Users selectedClient)
        {
            InitializeComponent();
            if (selectedClient != null)
                _currentClient = selectedClient;
            DataContext = _currentClient;

            var user = KursavayaEntities.GetContext().Users.FirstOrDefault(x => x.id.ToString() == AppString.id);
            if (user != null && user.ImageName != null)
            {
                string projectPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
                string imagesFolderPath = Path.Combine(projectPath, "Images");
                string destinationImagePath = Path.Combine(imagesFolderPath, user.ImageName);
                imageprofile.Source = new BitmapImage(new Uri(destinationImagePath));
            }
        }
        private void Button_Image_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files (*.jpg; *.png; *.jpeg)|*.jpg; *.png; *.jpeg|All Files (*.*)|*.*"; // Фильтр для отображения только изображений
            openFileDialog.Title = "Выберите изображение";

            if (openFileDialog.ShowDialog() == true)
            {
                string selectedImagePath = openFileDialog.FileName;

                string projectPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
                string imagesFolderPath = Path.Combine(projectPath, "Images");


                if (!Directory.Exists(imagesFolderPath))
                {
                    Directory.CreateDirectory(imagesFolderPath);
                }

                string imageFileName = Path.GetFileName(selectedImagePath);
                imgaefile = imageFileName;
                if (Directory.GetFiles(imagesFolderPath).Any(file => Path.GetFileName(file) == imageFileName))
                {
                    string existingImagePath = Path.Combine(imagesFolderPath, imageFileName);

                    BitmapImage bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.UriSource = new Uri(existingImagePath);
                    bitmap.EndInit();

                    imageprofile.Source = bitmap;
                    _currentClient.ImageName = imageFileName;
                }
                else
                {
                    string destinationImagePath = Path.Combine(imagesFolderPath, imageFileName);
                    File.Copy(selectedImagePath, destinationImagePath, false);
                    AppString.ImagePath = destinationImagePath;
                    imageprofile.Source = new BitmapImage(new Uri(destinationImagePath));
                }


                
                //var Userojb = KursavayaEntities.GetContext().Users.FirstOrDefault(x => x.id.ToString() == AppString.id);
                //Userojb.ImageName = imageFileName;
            }
        }
        private void btn_Back_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.MainFrame.GoBack();
        }
        private void btn_save_Click(object sender, RoutedEventArgs e)
        {
            int Roleid;
            StringBuilder errors = new StringBuilder();
            if (string.IsNullOrWhiteSpace(_currentClient.FIO))
            {
                errors.AppendLine("Введите ФИО");
            }
            if (string.IsNullOrWhiteSpace(_currentClient.address))
            {
                errors.AppendLine("Введите адресс");
            }
            if (string.IsNullOrWhiteSpace(_currentClient.phone_number))
            {
                errors.AppendLine("Введите номер телефона");
            }
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            var id = KursavayaEntities.GetContext().Users.FirstOrDefault(x => x.id == _currentClient.id);
            if (_currentClient != id)
            {
                try
                {
                    var RoleObj = KursavayaEntities.GetContext().Role.FirstOrDefault(x => x.name == AppString.Role);
                    Roleid = RoleObj.id;
                    Users userObj = new Users()
                    {
                        FIO = txbFIO.Text,
                        address = txbadr.Text,
                        phone_number = txbphone.Text,                       
                        login = AppString.login,
                        password = AppString.password,
                        Role_id = Roleid,
                        ImageName = imgaefile
                    };
                    KursavayaEntities.GetContext().Users.Add(userObj);
                    KursavayaEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные успешно добавлены!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    AppString.login = null;
                    AppString.password = null;
                    if (AppString.Role_name != "Админ")
                    {
                        AppFrame.MainFrame.Navigate(new PageLogin());
                    }
                    else
                    {
                        AppFrame.MainFrame.Navigate(new PageAllUsers());
                    }
                    
                }
                catch
                {
                    MessageBox.Show("Ошибка при добавлении данных!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("пользователь изменен!");
                AppFrame.MainFrame.GoBack();
            }
            
            try
            {
                KursavayaEntities.GetContext().SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            
        }
    }
}
