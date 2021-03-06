﻿using System.Windows;
using Minesweeper.Events;
using Prism.Events;
using Prism.Mvvm;

namespace Minesweeper.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private readonly IEventAggregator _eventAggregator;
        
        public MainWindowViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }
    }
}
