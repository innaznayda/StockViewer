using System;
using Akka.Actor;
using StockViewer.ActorModel.Messages;
using StockViewer.ExternalServices;

namespace StockViewer.ActorModel.Actors {
    internal class StockPriceLookupActor : ReceiveActor {
        private readonly IStockPriceServiceGateway StockPriceServiceGateway;

        public StockPriceLookupActor(IStockPriceServiceGateway stockPriceServiceGateway) {
            StockPriceServiceGateway = stockPriceServiceGateway;
            Receive<RefreshStockPriceMessage>(message => LookupStockPrice(message));
        }

        private void LookupStockPrice(RefreshStockPriceMessage message) {
            var latestPrice = StockPriceServiceGateway.GetLatestPrice(message.StockSymbol);
            //response
            Sender.Tell(new UpdatedStockPriceMessage(latestPrice, DateTime.Now));
        }
    }
}