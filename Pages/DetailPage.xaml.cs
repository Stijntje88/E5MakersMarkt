using E5MakersMarkt.Data;
using E5MakersMarkt.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace E5MakersMarkt.Pages
{
    public sealed partial class DetailPage : Page
    {
        private Product? _selectedProduct;
        private List<Comment> _allComments = new();

        public DetailPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            var clickedProduct = e.Parameter as Product;
            if (clickedProduct is null)
                return;

            using var db = new AppDbContext();

            var product = db.Products
                .Include(p => p.UserProduct)
                .ThenInclude(up => up.User)
                .FirstOrDefault(p => p.Id == clickedProduct.Id);

            _selectedProduct = product ?? clickedProduct;

            ProductListView.ItemsSource = _selectedProduct.UserProduct?.ToList();

            // 🔥 pas laden NA dat product is gezet
            LoadComment();
        }

        public static Microsoft.UI.Xaml.Media.Brush GetBackground(bool isReported)
        {
            return isReported 
                ? new Microsoft.UI.Xaml.Media.SolidColorBrush(Microsoft.UI.Colors.LightCoral)
                : new Microsoft.UI.Xaml.Media.SolidColorBrush(Microsoft.UI.Colors.Transparent);
        }

        public static bool IsNotReported(bool isReported) => !isReported;

        private void LoadComment()
        {
            using var db = new AppDbContext();

            if (_selectedProduct == null)
                return;

            _allComments = db.Comments
                .Include(c => c.User) // nodig voor Username
                .Where(c => c.ProductId == _selectedProduct.Id) // filter op product
                .ToList();

            commentlistView.ItemsSource = _allComments;
        }

        private void AddComment_Click(object sender, RoutedEventArgs e)
        {
            using var db = new AppDbContext();

            if (_selectedProduct == null)
                return;

            // veilige rating parsing
            if (!int.TryParse(CommentRating.Text, out int rating))
            {
                // eventueel foutmelding tonen
                return;
            }

            var newComment = new Comment
            {
                CommentTitle = CommentTitle.Text,
                Description = CommentDescription.Text,
                Rating = rating,
                UserId = 1, // later vervangen door ingelogde user
                ProductId = _selectedProduct.Id
            };

            db.Comments.Add(newComment);
            db.SaveChanges();

            // velden leegmaken (mooie UX)
            CommentTitle.Text = "";
            CommentDescription.Text = "";
            CommentRating.Text = "";

            LoadComment();
        }



        private void Home_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(HomePages));
        }

        private void Reported_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Microsoft.UI.Xaml.Controls.Button;
            var selectedUserProduct = btn?.DataContext as UserProduct ?? ProductListView.SelectedItem as UserProduct;

            if (selectedUserProduct == null)
                return;

            using var db = new AppDbContext();
            var userProduct = db.UserProducts
                .FirstOrDefault(up => up.Id == selectedUserProduct.Id);

            if (userProduct == null)
                return;

            userProduct.Reported = true;
            selectedUserProduct.Reported = true;

            db.SaveChanges();

            ProductListView.ItemsSource = null;
            ProductListView.ItemsSource = _selectedProduct?.UserProduct?.ToList();
        }

        private void Order_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(OrderPage), _selectedProduct);
        }
    }
}