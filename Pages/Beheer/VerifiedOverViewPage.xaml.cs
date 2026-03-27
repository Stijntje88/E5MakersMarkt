using E5MakersMarkt.Data;
using E5MakersMarkt.Data.Models;
using E5MakersMarkt.Pages.Login;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace E5MakersMarkt.Pages.Beheer
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class VerifiedOverViewPage : Page
    {
        public VerifiedOverViewPage()
        {
            InitializeComponent();
            LoadUser();
        }


        private void LoadUser()
        {
            using var db = new AppDbContext();

            var user = db.Users
                .Where(s => s.Satus == "Pending")
                .ToList();

            BeheerUserList.ItemsSource = user;
        }

        private void userSearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var searchQuery = userSearchTextBox.Text.Trim().ToLower();

            using var db = new AppDbContext();

            var user = db.Users
               .Where(u => u.Username.Contains(searchQuery) && u.Satus == "Pending")
               .ToList();

            BeheerUserList.ItemsSource = user;
        }

        private void BeheerOverviewButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(BeheerOverViewPage));
        }

        private void UserButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(BeheerUserOverViewPage));
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(LoginOverViewPages));
        }

        private void Verified_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(VerifiedOverViewPage));
        }

        private void VerfiedUsers_Click(object sender, RoutedEventArgs e)
        {
            using var db = new AppDbContext();

            var button = sender as Button;
            var inputUser = button?.DataContext as User;

            var userId = inputUser.Id;

            var user = db.Users.FirstOrDefault(u => u.Id == userId);

            if (user != null)
            {
                user.Satus = "Verified";
            }

            db.SaveChanges();

            var dialog = new ContentDialog
            {
                Title = "Gelukt!",
                Content = "Gebruiker is Geverfierd.",
                CloseButtonText = "OK",
                XamlRoot = this.XamlRoot
            };

            _ = dialog.ShowAsync();

            Frame.GoBack();
        }
    }
}
