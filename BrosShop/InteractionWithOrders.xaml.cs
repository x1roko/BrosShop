using BrosShop.Context;
using BrosShop.Models;
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

namespace BrosShop
{
    /// <summary>
    /// Логика взаимодействия для InteractionWithOrders.xaml
    /// </summary>
    public partial class InteractionWithOrders : Window
    {
        
        public InteractionWithOrders()
        {
            InitializeComponent();
            LoadWindow();
        }

        public void LoadWindow()
        {
            wbCheckBox.IsChecked = true;
            cassaCheckBox.IsChecked = true;
            siteCheckBox.IsChecked = true;
        }

        public void LoadOrders()
        {
            try
            {
                Context.BrosShopDbContext context = new();
                List<BrosShopOrder> orders = [];
                if (wbCheckBox.IsChecked.Value)
                    orders.AddRange(context.BrosShopOrders.Where(o => o.BrosShopTypeOrder == "WB"));
                if (cassaCheckBox.IsChecked.HasValue)
                    orders.AddRange(context.BrosShopOrders.Where(o => o.BrosShopTypeOrder == "касса"));
                if (siteCheckBox.IsChecked.HasValue)
                    orders.AddRange(context.BrosShopOrders.Where(o => o.BrosShopTypeOrder == "веб-сайт"));
                orders.OrderBy(o => o.BrosShopOrderId);
                ordersListView.ItemsSource = orders;
            }
            catch (Exception)
            {
            }
        }

        private void TypeOrderCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            LoadOrders();
        }

        private void CreateOrder_Click(object sender, RoutedEventArgs e)
        {
            try
            {

            }
            catch (Exception)
            {
                throw;
            }
        }

        private void showStatistics_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
