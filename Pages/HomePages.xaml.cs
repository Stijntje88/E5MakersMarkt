using E5MakersMarkt.Data;
using E5MakersMarkt.Data.Models;
using E5MakersMarkt.Data.Session;
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

namespace E5MakersMarkt.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class HomePages : Page
    {
        private List<Product> _allProducts;

        public HomePages()
        {
            this.InitializeComponent();
            LoadProducts();
        }

        private void LoadProducts()
        {
            using var db = new AppDbContext();
            _allProducts = db.Products.ToList();
            ItemList.ItemsSource = _allProducts;
        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var searchText = SearchBox.Text.ToLower().Trim();

            if (string.IsNullOrEmpty(searchText))
            {
                ItemList.ItemsSource = _allProducts;
            }
            else
            {
                var filteredItems = _allProducts.Where(item =>
                    item.Name?.ToLower().Contains(searchText) == true ||
                    item.Description?.ToLower().Contains(searchText) == true ||
                    item.Type?.ToLower().Contains(searchText) == true ||
                    item.Material?.ToLower().Contains(searchText) == true ||
                    item.ProductionTime?.ToLower().Contains(searchText) == true
                ).ToList();

                ItemList.ItemsSource = filteredItems;
            }
        }

        private void LogOut_Click(object sender, RoutedEventArgs e)
        {
            CurrentSession.LoggedInUser = null;
            Frame.Navigate(typeof(LoginOverViewPages));
        }

        private void ProductList_ItemClick(object sender, ItemClickEventArgs e)
        {
            var Product = (Product)e.ClickedItem;
            //var CitizenId = Citizen.Id;

            Frame.Navigate(typeof(DetailPage), Product);
        }
    }
}
