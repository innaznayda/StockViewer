using Akka.Actor;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace StockViewer.ViewModel {
    class StoclToggleButtonViewModel : ViewModelBase {
        private string buttonText;
        public string StockSymbol { get; set; }

        public ICommand ToggleCommand { get; set; }

        public IActorRef StockToggleButtonActorRef { get; private set; }

       
    }
}
