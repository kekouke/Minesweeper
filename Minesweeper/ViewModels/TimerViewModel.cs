using System;
using Prism.Events;
using Prism.Mvvm;
using System.Windows.Threading;
using Minesweeper.Events;

namespace Minesweeper.ViewModels
{
    public class TimerViewModel : BindableBase
    {
        public uint TotalSeconds { get; set; }

        private readonly DispatcherTimer timer;

        public TimerViewModel(IEventAggregator eventAggregator)
        {
            eventAggregator.GetEvent<FirstCellClickEvent>().Subscribe(() => timer.Start());
            eventAggregator.GetEvent<GameOverEvent>().Subscribe(() => timer.Stop());
            eventAggregator.GetEvent<StartNewGameEvent>().Subscribe(() => {
                timer.Stop();
                ResetTimer();
            });

            timer = CreateTimer();
        }

        private void ResetTimer()
        { 
            TotalSeconds = 0;   
            RaisePropertyChanged("TotalSeconds");
        }

        private DispatcherTimer CreateTimer()
        {
            var t = new DispatcherTimer();

            t.Tick += delegate {
                TotalSeconds += 1;
                RaisePropertyChanged("TotalSeconds");
            };

            t.Interval = new TimeSpan(0, 0, 1);

            return t;
        }
    }
}