using System;

namespace StockViewer.ExternalServices {
    class RandomStockPriceServiceGateway : IStockPriceServiceGateway {
        private decimal LastRandomPrice = 20;
        private readonly Random random = new Random();

        public decimal GetLatestPrice(string stockSymbol) {
            var newPrice = LastRandomPrice + random.Next(-5, 5);
            if (newPrice < 0) {
                newPrice = 5;
            } else if (newPrice > 50) {
                newPrice = 45;
            }
            LastRandomPrice = newPrice;

            return newPrice;
        }
    }
}
