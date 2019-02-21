using Akka.Actor;
using StockViewer.ActorModel.Messages;
using System;
using System.Collections.Generic;

namespace StockViewer.ActorModel.Actors {
    class StockCoordinatorActor : ReceiveActor {

        private readonly IActorRef ChartingActor;
        private readonly Dictionary<string, IActorRef> StockActors;

        public StockCoordinatorActor(IActorRef chartingActor) {
            ChartingActor = chartingActor;
            StockActors = new Dictionary<string, IActorRef>();

            Receive<WatchStockMessage>(message => WatchStock(message));
            Receive<UnWatchStockMessage>(message => UnWatchStock(message));
        }

        private void UnWatchStock(UnWatchStockMessage message) {
           if (!StockActors.ContainsKey(message.StockSymbol)){
                return;
            }
            ChartingActor.Tell(new RemoveChartSeriesMessage(message.StockSymbol));
            StockActors[message.StockSymbol].Tell(new UnsibscibeFromNewStockPriceMessage(ChartingActor));
        }

        private void WatchStock(WatchStockMessage message) {
            bool childActorNeedsCreating = !StockActors.ContainsKey(message.StockSymbol);
            if (childActorNeedsCreating) {
                IActorRef newChildActor = Context.ActorOf(Props.Create(() => new StockActor(message.StockSymbol)), "StockActor_" + message.StockSymbol);
                StockActors.Add(message.StockSymbol,newChildActor);
            }

            ChartingActor.Tell(new AddChartSeriesMessage(message.StockSymbol));
            StockActors[message.StockSymbol].Tell(new SubscibeToNewStockPriceMessage(ChartingActor));
        }
    }
}
