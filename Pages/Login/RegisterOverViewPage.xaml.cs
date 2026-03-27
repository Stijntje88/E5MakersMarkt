using E5MakersMarkt.Data;
using E5MakersMarkt.Data.Models;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.WindowsAppSDK.Runtime.Packages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;


// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace E5MakersMarkt.Pages.Login
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class RegisterOverViewPage : Page
    {
        public RegisterOverViewPage()
        {
            InitializeComponent();
        }


        private async void RegisterButton_Click(object sender, RoutedEventArgs e)
        {

            string username = UsernameTextBox.Text;
            string password = PasswordTextBox.Password;

            if (string.IsNullOrWhiteSpace(username) ||
                string.IsNullOrWhiteSpace(password))
            {
                ErrorText.Text = "Vul alle velden in.";
                return;
            }

            using var db = new AppDbContext();

            var existingUser = db.Users.FirstOrDefault(u => u.Username == username);

            if (existingUser != null)
            {
                ErrorText.Text = "Gebruikersnaam is al in gebruik.";
                return;
            }
            if (password.Length < 6)
            {
                ErrorText.Text = "Wachtwoord moet minimaal 6 tekens lang zijn.";
                return;
            }

            if (username.Length < 3)
            {
                ErrorText.Text = "Gebruikersnaam moet minimaal 3 tekens lang zijn.";
                return;
            }

            if (username.Contains(" "))
            {
                ErrorText.Text = "Gebruikersnaam mag geen spaties bevatten.";
                return;
            }

            var newUser = new User
            {
                Username = username,
                Password = BCrypt.Net.BCrypt.HashPassword(password),
                Role = "user",
                Status = "Pending",

            };

            db.Users.Add(newUser);
            db.SaveChanges();
            ContentDialog dialog = new ContentDialog
            {
                XamlRoot = this.XamlRoot,
                Title = "Registratie is voltooid",
                Content = "Je account is succesvol aangemaakt.",
                CloseButtonText = "OK"
            };

            await dialog.ShowAsync();

            Frame.Navigate(typeof(LoginOverViewPages));
        }
        private void Login_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(LoginOverViewPages));
        }
        
    }
}
