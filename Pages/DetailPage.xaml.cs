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
        public DetailPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            var ClickedProduct = (Product)e.Parameter;

            using var db = new AppDbContext();

            var Product = db.Products
                        .Include(c => c.UserProduct)
                        .ThenInclude(bc => bc.User)
                        .FirstOrDefault(c => c.Id == ClickedProduct.Id);
        }

            //if (Product != null)
            //{
            //    idTextBox.Text = citizen.Id.ToString();
            //    nameTextBox.Text = citizen.Name;
            //    jobTextBox.Text = citizen.Job;
            //    buildingsListView.ItemsSource = citizen.BuildingCitizen.ToList();

            //    // Laat de gebouwen zien via de many-to-many relatie
            //    //buildingsListView.ItemsSource = citizen.BuildingCitizen
            //    //                                       .Select(bc => bc.Building)
            //    //                                       .ToList();
            //}
            private void Home_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(HomePages));
        }
    }
}

