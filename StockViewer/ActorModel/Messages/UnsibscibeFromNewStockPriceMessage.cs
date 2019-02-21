using Akka.Actor;

namespace StockViewer.ActorModel.Messages {
    class UnsibscibeFromNewStockPriceMessage {
        public IActorRef Subscriber { get; private set; }
        public UnsibscibeFromNewStockPriceMessage(IActorRef unsibsribingActor) {
            Subscriber = unsibsribingActor;
        }

    }
}
