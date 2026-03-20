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

            private void RegisterButton_Click(object sender, RoutedEventArgs e)
            {
            string username = UsernameTextBox.Text;
            string password = PasswordTextBox.Text;

            if (string.IsNullOrWhiteSpace(username) ||
                string.IsNullOrWhiteSpace(password) )
            {
                ErrorText.Text = "Vul alle velden in.";
                return;
            }

            var newUser = new User
            {
                Username = username,
                Password = BCrypt.Net.BCrypt.HashPassword(password),
                Role = "user",
               
            };
            using var db = new AppDbContext();
            db.Users.Add(newUser);
            db.SaveChanges();
            Frame.Navigate(typeof(LoginOverViewPages));
        }
    }
}
