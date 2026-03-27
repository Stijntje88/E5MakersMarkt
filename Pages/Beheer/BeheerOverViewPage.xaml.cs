using E5MakersMarkt.Data;
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

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace E5MakersMarkt.Pages.Beheer
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class BeheerOverViewPage : Page
    {
        public BeheerOverViewPage()
        {
            InitializeComponent();
            LoadDashBoard();
        }

        private void LoadDashBoard()
        {
            using var db = new AppDbContext();

            var totalUsers = db.Users.Count();
            var totalVerifiedUser = db.Users.Count(s => s.Satus == "Verified");

            TotalUsersText.Text = totalUsers.ToString();
            TotalVerfiedUser.Text = totalVerifiedUser.ToString();
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
    }
}
