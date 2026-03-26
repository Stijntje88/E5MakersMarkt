using E5MakersMarkt.Data;
using E5MakersMarkt.Data.Models;
using E5MakersMarkt.Pages.Login;
using Microsoft.EntityFrameworkCore;
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
    public sealed partial class DetailPage : Page
    {
        private Product? _selectedProduct;

        public DetailPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            var clickedProduct = e.Parameter as Product;
            if (clickedProduct is null)
            {
                return;
            }

            using var db = new AppDbContext();

            var product = db.Products
                        .Include(c => c.UserProduct)
                        .ThenInclude(bc => bc.User)
                        .FirstOrDefault(c => c.Id == clickedProduct.Id);

            _selectedProduct = product ?? clickedProduct;
            ProductListView.ItemsSource = product?.UserProduct?.ToList();
        }

        private void Home_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(HomePages));
        }

        private void Order_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(OrderPage), _selectedProduct);
        }
    }
}

