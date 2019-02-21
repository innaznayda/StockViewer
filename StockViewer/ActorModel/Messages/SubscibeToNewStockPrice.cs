using Akka.Actor;

namespace StockViewer.ActorModel.Messages {
    class SubscibeToNewStockPriceMessage {

        public IActorRef Subscriber { get; private set; }
        public SubscibeToNewStockPriceMessage(IActorRef subscriber) {
            Subscriber = subscriber;
        }
    }
}
