using Akka.Actor;
using Akka.DI.Core;
using StockViewer.ActorModel.Messages;
using System.Collections.Generic;

namespace StockViewer.ActorModel.Actors {
    class StockActor : ReceiveActor {
        private readonly string StockSymbol;
        private readonly HashSet<IActorRef> Subscribers;
        private readonly IActorRef PriceLookupChild;
        private decimal StockPrice;

        public StockActor(string stock) {
            StockSymbol = stock;
            Subscribers = new HashSet<IActorRef>();
            PriceLookupChild = Context.ActorOf(Context.DI().Props<StockPriceLookupActor>());
            Receive<SubscibeToNewStockPriceMessage>(message=> Subscribers.Add(message.Subscriber));
            Receive<UnsibscibeFromNewStockPriceMessage>(message => Subscribers.Remove(message.Subscriber));
            Receive<RefreshStockPriceMessage>(message => PriceLookupChild.Tell(message));
            Receive<UpdatedStockPriceMessage>(message => {
                StockPrice = message.Price;
                var stockPriceMessage = new StockPriceMessage(StockSymbol, StockPrice, message.Date);
                foreach (var subscriber in Subscribers) {
                    subscriber.Tell(stockPriceMessage);
                }
            });
        }
    }
}
