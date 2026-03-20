using E5MakersMarkt.Data;
using E5MakersMarkt.Pages.Beheer;
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

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace E5MakersMarkt.Pages.Login
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LoginOverViewPages : Page
    {
        public LoginOverViewPages()
        {
            InitializeComponent();
        }


        private void AttemptLogin()
        {
            string enterdUsername = UsernameBox.Text.Trim();
            string enterdPassword = PasswordBox.Password;

            if (string.IsNullOrEmpty(enterdUsername) || string.IsNullOrEmpty(enterdPassword))
            {
                ShowError("Een van de gevenens zijn niet ingevuld!");
                return;
            }

            using var db = new AppDbContext();

            var user = db.Users.FirstOrDefault(u => u.Username == enterdUsername);
            if (user == null || !BCrypt.Net.BCrypt.Verify(enterdPassword, user.Password))
            {
                ShowError("Ongeldige gebruikersnaam of wachtwoord!");
                return;

                PasswordBox.Password = string.Empty;
            }
            else
            {
                if(user.Role == "admin")
                {
                    Frame.Navigate(typeof(BeheerOverViewPage));
                }
                if(user.Role == "user")
                {
                    Frame.Navigate(typeof(HomePages));
                }
                else
                {
                    ShowError("Rol is niet gevonden neem contact met het beheer op");
                }
            }
        }

        private void DevLoginPlayer_Click(object sender, RoutedEventArgs e)
        {
            var username = "admin";
            var password = "admin123";

            using var db = new AppDbContext();

            var user = db.Users.FirstOrDefault(u =>
            u.Username.ToLower() == username.ToLower());

            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.Password))
            {
                ShowError("⚠ ACCESS DENIED: Incorrect Wachtwoord!");

            }
            else
            {
                Frame.Navigate(typeof(BeheerOverViewPage));
            }
        }

        private void ShowError(string message)  
        {
            ErrorMessage.Text = message;  
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            AttemptLogin();
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(RegisterOverViewPage));
        }
    }
}
