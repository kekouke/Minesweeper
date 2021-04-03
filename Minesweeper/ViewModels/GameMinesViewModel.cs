using Minesweeper.Events;
using Minesweeper.Models;
using Prism.Events;
using Prism.Mvvm;

namespace Minesweeper.ViewModels
{
    public class GameMinesViewModel : BindableBase
    {
        private int _minesCount;
        public int CurrentMinesCount { get; private set; }
        
        public GameMinesViewModel(IEventAggregator eventAggregator)
        {
            eventAggregator.GetEvent<CellFlagEvent>().Subscribe(OnFlagged);
            eventAggregator.GetEvent<ChangeGameConfigEvent>().Subscribe(OnStartNewGame);
            eventAggregator.GetEvent<RestartGameEvent>().Subscribe(OnRestartGame);
        }

        private void OnRestartGame()
        {
            UpdateCurrentMinesCount(_minesCount);
        }

        private void OnStartNewGame(GameConfiguration configuration)
        {
            _minesCount = configuration.MinesCount;
            OnRestartGame();
        }

        private void OnFlagged(int countMines)
        {
            UpdateCurrentMinesCount(countMines);
        }

        private void UpdateCurrentMinesCount(int count)
        {
            CurrentMinesCount = count;
            RaisePropertyChanged("CurrentMinesCount");
        }
    }
}