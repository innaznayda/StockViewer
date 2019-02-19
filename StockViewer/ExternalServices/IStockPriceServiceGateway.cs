namespace StockViewer.ExternalServices
{
    interface IStockPriceServiceGateway
    {
        decimal GetLatestPrice(string stockSymbol);
    }
}
