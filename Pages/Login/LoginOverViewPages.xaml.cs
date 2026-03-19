using E5MakersMarkt.Data;
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
                 Frame.Navigate(typeof(HomePages));
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
    }
}
