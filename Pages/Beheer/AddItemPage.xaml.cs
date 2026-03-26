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

    private void Save_Click(object sender, RoutedEventArgs e)
    {
        Product item = new Product
        {
            Id = 1,
            Name = ItemNameBox.Text,
            Description = DescriptionBox.Text,
            Type = TypeBox.Text,
            Material = MaterialBox.Text,
            ProductionTime = ProductionTimeBox.Text,
        };

        db.Products.Add(item);
        db.SaveChanges();

        Frame.GoBack();
    }
}
