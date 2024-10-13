using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrosShop
{
    public class BrosShopProductsModel : INotifyPropertyChanged
    {
        public int BrosShopProductId { get; set; }

        public decimal BrosShopPrice { get; set; }

        public string BrosShopTitle { get; set; } = null!;

        public int? BrosShopDiscountPercent { get; set; }

        public string? BrosShopDescription { get; set; }

        public string BrosShopCategory { get; set; }

        public int? BrosShopAttributeId { get; set; }

        private int _BrosShopCount;
        public int BrosShopCount
        {
            get => _BrosShopCount;
            set
            {
                if (_BrosShopCount != value)
                {
                    _BrosShopCount = value;
                    OnPropertyChanged(nameof(BrosShopCount));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
