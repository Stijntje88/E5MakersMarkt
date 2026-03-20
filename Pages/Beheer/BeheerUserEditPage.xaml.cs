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
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Security.Authentication.OnlineId;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace E5MakersMarkt.Pages.Beheer
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class BeheerUserEditPage : Page
    {
        public int userId;
        public BeheerUserEditPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var user = (User)e.Parameter;
            userId = user.Id;

            NameTextBox.Text = user.Username;
            
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

        private void SaveChange_Click(object sender, RoutedEventArgs e)
        {
            using var db = new AppDbContext();

            var user = db.Users.FirstOrDefault(u => u.Id == userId);

            if (user != null)
            {
                user.Username = NameTextBox.Text.Trim(); ;

                var enterdPassword = PasswordTextBox.Password.Trim();
                if (!string.IsNullOrEmpty(enterdPassword))
                {
                    var hashedPassowrd = BCrypt.Net.BCrypt.HashPassword(enterdPassword);
                    user.Password = hashedPassowrd;
                }

                db.SaveChanges();

                var dialog = new ContentDialog
                {
                    Title = "Gelukt!",
                    Content = "Gebruiker is bijgewerkt.",
                    CloseButtonText = "OK",
                    XamlRoot = this.XamlRoot
                };

                _ = dialog.ShowAsync();

                Frame.GoBack();
            }
        }
    }
}
