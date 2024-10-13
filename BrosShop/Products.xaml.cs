using BrosShop.Models;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace BrosShop
{
    /// <summary>
    /// Логика взаимодействия для Products.xaml
    /// </summary>
    public partial class Products : Window
    {
        List<BrosShopProductsModel> products = [];
        public Products()
        {
            InitializeComponent();
            LoadCategories();
            LoadProducts();
        }

        public void LoadProducts()
        {
            using Context.BrosShopDbContext context = new();
            var productsQuery = context.BrosShopProducts
            .Select(p => new BrosShopProductsModel
            {
                BrosShopProductId = p.BrosShopProductId,
                BrosShopTitle = p.BrosShopTitle,
                BrosShopPrice = p.BrosShopPrice,
                BrosShopCategory = p.BrosShopCategory.BrosShopCategoryTitle,
                BrosShopAttributeId = p.BrosShopProductAttributes.Select(p => p.BrosShopAttributesId).FirstOrDefault(),
                BrosShopDescription = p.BrosShopDescription

            });
            
            products = productsQuery.ToList();

            productsListView.ItemsSource = products;
        }

        /*var products = context.BrosShopProducts.Select(p =>
                new BrosShopProduct { BrosShopTitle = p.BrosShopTitle, BrosShopPrice = p.BrosShopPrice })*;*/

        public void LoadCategories()
        {
            using Context.BrosShopDbContext context = new();
            var categories = context.BrosShopCategories.Select(c =>
                new BrosShopCategory { BrosShopCategoryTitle = c.BrosShopCategoryTitle});
              
            categoryListView.ItemsSource = categories.ToList();
        }

        private void ShowCartButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Decrement_Click(object sender, RoutedEventArgs e)
        {

            /*if (products[((ProductModel)productsListView.SelectedItem).product_id - 1].Count > 0)
                products[((ProductModel)productsListView.SelectedItem).product_id - 1].Count--;*/
        }

        private void Increment_Click(object sender, RoutedEventArgs e)
        {
            /*if (products[((ProductModel)productsListView.SelectedItem).product_id - 1].Count < 10)
                products[((ProductModel)productsListView.SelectedItem).product_id - 1].Count++;*/
        }
    }
}
