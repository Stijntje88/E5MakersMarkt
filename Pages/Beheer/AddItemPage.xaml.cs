using E5MakersMarkt.Data;
using E5MakersMarkt.Data.Models;
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

namespace E5MakersMarkt.Pages;

/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class AddItemPage : Page
{
    private AppDbContext db = new AppDbContext();

    public AddItemPage()
    {
        InitializeComponent();
    }

    private async void Save_Click(object sender, RoutedEventArgs e)
    {
        // Controle: zijn alle velden ingevuld?
        if (string.IsNullOrWhiteSpace(ItemNameBox.Text) ||
            string.IsNullOrWhiteSpace(DescriptionBox.Text) ||
            string.IsNullOrWhiteSpace(ImgBox.Text) ||
            string.IsNullOrWhiteSpace(TypeBox.Text) ||
            string.IsNullOrWhiteSpace(MaterialBox.Text) ||
            string.IsNullOrWhiteSpace(ProductionTimeBox.Text))
        {
            ContentDialog dialog = new ContentDialog
            {
                Title = "Fout",
                Content = "Vul alle velden in voordat je opslaat.",
                CloseButtonText = "OK",
                XamlRoot = this.Content.XamlRoot // voorkomt crash
            };

            await dialog.ShowAsync();
            return;
        }

        try
        {
            // Opslaan in database
            Product item = new Product
            {
                Name = ItemNameBox.Text,
                Description = DescriptionBox.Text,
                Img = ImgBox.Text,
                Type = TypeBox.Text,
                Material = MaterialBox.Text,
                ProductionTime = ProductionTimeBox.Text,
            };

            db.Products.Add(item);
            db.SaveChanges();

            // Terug naar vorige pagina
            Frame.GoBack();
        }
        catch (Exception)
        {
            // Fout bij opslaan → melding tonen
            ContentDialog errorDialog = new ContentDialog
            {
                Title = "Error",
                Content = "Er ging iets mis bij het opslaan.",
                CloseButtonText = "OK",
                XamlRoot = this.Content.XamlRoot
            };

            await errorDialog.ShowAsync();
        }
    }
}
