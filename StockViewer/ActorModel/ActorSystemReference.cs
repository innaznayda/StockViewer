using System;
using Akka.Actor;
using Akka.DI.Core;
using Akka.DI.Ninject;
using Ninject;

namespace StockViewer.ActorModel
{
    static class ActorSystemReference
    {
        public static ActorSystem ActorSystem { get; private set; }

        static ActorSystemReference()
        {
            CreateActorSystem();
        }

        private static void CreateActorSystem()
        {
            ActorSystem = ActorSystem.Create("StockActorSystem");
            var container = new StandardKernel();
            IDependencyResolver resolver = new NinjectDependencyResolver(container, ActorSystem);
        }
    }
}
