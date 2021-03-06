using Minesweeper.Events;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;

namespace Minesweeper.ViewModels
{
    public class GameMenuViewModel : BindableBase
    {
        private IEventAggregator _eventAggregator;
        
        public DelegateCommand RestartGameCommand { get; private set; }

        public GameMenuViewModel(IEventAggregator eventAggregator)
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