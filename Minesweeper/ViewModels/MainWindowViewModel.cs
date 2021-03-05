using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Minesweeper.Events;
using Prism.Commands;

namespace Minesweeper.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private readonly IEventAggregator _eventAggregator;
        
        public DelegateCommand RestartGameCommand { get; private set; }
        
        public MainWindowViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            RestartGameCommand = new DelegateCommand(RestartGame);
        }

        private void RestartGame()
        {
            _eventAggregator.GetEvent<RestartGameEvent>().Publish();
        }
    }
}
