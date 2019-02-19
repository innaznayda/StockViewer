namespace StockViewer.ActorModel.Messages
{
    class RefreshStockPriceMessage
    {
        public string StockSymbol { get; private set; }

        public RefreshStockPriceMessage(string stock)
        {
            StockSymbol = stock;
        }
    }
}
