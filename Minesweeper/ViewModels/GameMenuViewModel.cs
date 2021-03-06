using Minesweeper.Events;
using Minesweeper.Services;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;

namespace Minesweeper.ViewModels
{
    public class GameMenuViewModel : BindableBase
    {
        private IEventAggregator _eventAggregator;
        private IGameConfigurationService _configService;
        
        public DelegateCommand RestartGameCommand { get; }
        public DelegateCommand<string> ChangeGameConfigCommand { get; }

        public GameMenuViewModel(IEventAggregator eventAggregator, IGameConfigurationService configSrvice)
        {
            _eventAggregator = eventAggregator;
            _configService = configSrvice;
            
            RestartGameCommand = new DelegateCommand(RestartGame);
            ChangeGameConfigCommand = new DelegateCommand<string>(ChangeGameConfig);
        }

        private void ChangeGameConfig(string configName)
        {
            var configGame = Settings.DefaultGameConfig;
            
            if (configName == "Beginner")
            {
                 configGame = _configService.GetBeginnerConfig;
            }

            if (configName == "Advanced")
            {
                 configGame = _configService.GetAdvancedConfig;
            }

            if (configName == "Expert")
            {
                 configGame = _configService.GetExpertConfig;
            }
            
            _eventAggregator.GetEvent<ChangeGameConfigEvent>().Publish(configGame);
        }

        private void RestartGame()
        {
            _eventAggregator.GetEvent<RestartGameEvent>().Publish();
        }
    }
}