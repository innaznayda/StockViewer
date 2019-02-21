using Akka.Actor;
using Akka.DI.Core;
using StockViewer.ActorModel.Messages;
using System;
using System.Collections.Generic;

namespace StockViewer.ActorModel.Actors {
    class StockActor : ReceiveActor {
        private readonly string StockSymbol;
        private readonly HashSet<IActorRef> Subscribers;
        private readonly IActorRef PriceLookupChild;
        private decimal StockPrice;
        private ICancelable PriceRefreshing;

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

        protected override void PreStart() {
            PriceRefreshing = Context.System.Scheduler.
                ScheduleTellRepeatedlyCancelable(TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(1), Self, new RefreshStockPriceMessage(StockSymbol), Self);
        }
        protected override void PostStop() {
            PriceRefreshing.Cancel(false);
            base.PostStop();
        }
    }
}
