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
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace E5MakersMarkt.Pages.Beheer;

/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class BeheerUserOverViewPage : Page
{
    public BeheerUserOverViewPage()
    {
        InitializeComponent();
        LoadUser();
    }

    private void LoadUser()
    {
        using var db = new AppDbContext();

        var user = db.Users
            .ToList();

        BeheerUserList.ItemsSource = user;
    }

    private void userSearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
    {
        var searchQuery = userSearchTextBox.Text.Trim().ToLower();

        using var db = new AppDbContext();

        var user = db.Users
           .Where(u => u.Username.Contains(searchQuery))
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

    private async void DeleteUserButton_Click(object sender, RoutedEventArgs e)
    {
        using var db = new AppDbContext();

        var button = sender as Button;
        var userId = button?.DataContext as User;

        if (userId != null)
        {
            ContentDialog confirmDialog = new ContentDialog
            {
                Title = "Bestig Verwijderen",
                Content = $"Weet je zeker dat je '{userId.Username}' wilt verwijderen?",
                PrimaryButtonText = "Verwijderen",
                CloseButtonText = "Anuleren",
                XamlRoot = this.XamlRoot
            };

            var result = await confirmDialog.ShowAsync();

            if (result == ContentDialogResult.Primary)
            {
                db.Users.Remove(userId);
                db.SaveChanges();
                LoadUser();
                var dialog = new ContentDialog
                {
                    Title = "Verwijderd!",
                    Content = "Gebruiker is verwijderd.",
                    CloseButtonText = "OK",
                    XamlRoot = this.XamlRoot
                };
                await dialog.ShowAsync();
            }
        }
    }

    private void Edit_Click(object sender, RoutedEventArgs e)
    {
        using var db = new AppDbContext();

        var button = sender as Button;
        var user = button?.DataContext as User;
        Frame.Navigate(typeof(BeheerUserEditPage), user);
    }
}
