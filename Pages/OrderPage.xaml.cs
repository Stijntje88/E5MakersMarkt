using E5MakersMarkt.Data;
using E5MakersMarkt.Data.Models;
using E5MakersMarkt.Data.Session;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Globalization;

namespace E5MakersMarkt.Pages;

public sealed partial class OrderPage : Page
{
    private Product? _selectedProduct;

    public OrderPage()
    {
        InitializeComponent();
    }

    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
        base.OnNavigatedTo(e);

        _selectedProduct = e.Parameter as Product;
        ProductNameText.Text = _selectedProduct is null
            ? "Product: -"
            : $"Product: {_selectedProduct.Name}";

        UsernameText.Text = CurrentSession.LoggedInUser is null
            ? "Ingelogd als: -"
            : $"Ingelogd als: {CurrentSession.LoggedInUser.Username}";
    }

    private async void PlaceOrder_Click(object sender, RoutedEventArgs e)
    {
        if (_selectedProduct is null)
        {
            StatusText.Text = "Geen product geselecteerd.";
            return;
        }

        if (CurrentSession.LoggedInUser is null)
        {
            StatusText.Text = "Je bent niet ingelogd.";
            return;
        }

        var validPrice = float.TryParse(PriceTextBox.Text, NumberStyles.Float, CultureInfo.CurrentCulture, out var price)
            || float.TryParse(PriceTextBox.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out price);

        if (!validPrice || price <= 0)
        {
            StatusText.Text = "Voer een geldige prijs in (groter dan 0).";
            return;
        }

        using var db = new AppDbContext();

        var order = new UserProduct
        {
            UserId = CurrentSession.LoggedInUser.Id,
            ProductId = _selectedProduct.Id,
            Datum = DateTime.Now,
            Price = price,
            Reported = false
        };

        db.UserProducts.Add(order);
        db.SaveChanges();

        ContentDialog dialog = new ContentDialog
        {
            XamlRoot = this.XamlRoot,
            Title = "Bestelling geplaatst",
            Content = $"{CurrentSession.LoggedInUser.Username} heeft {_selectedProduct.Name} besteld voor {price:0.00}.",
            CloseButtonText = "OK"
        };

        await dialog.ShowAsync();

        Frame.Navigate(typeof(HomePages));
    }

    private void GoBack_Click(object sender, RoutedEventArgs e)
    {
        Frame.Navigate(typeof(HomePages));
    }
}
