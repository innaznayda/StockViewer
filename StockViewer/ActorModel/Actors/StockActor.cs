using Akka.Actor;
using StockViewer.ActorModel.Messages;
using System.Collections.Generic;

namespace StockViewer.ActorModel.Actors {
    class StockActor : ReceiveActor{
        private readonly string StockSymbol;
        private readonly HashSet<IActorRef> Subscribers;


        public StockActor(string stock) {
            StockSymbol = stock;
            Subscribers = new HashSet<IActorRef>();
            Receive<SubscibeToNewStockPriceMessage>(message=> Subscribers.Add(message.Subscriber));
            Receive<UnsibscibeFromNewStockPriceMessage>(message => Subscribers.Remove(message.Subscriber));
        }


    }
}
