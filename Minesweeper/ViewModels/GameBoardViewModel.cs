using System;
using System.Windows;
using Minesweeper.Enums;
using Minesweeper.Events;
using Minesweeper.Models;
using Minesweeper.Services;
using Prism.Events;
using Prism.Mvvm;

namespace Minesweeper.ViewModels
{
    public class GameBoardViewModel : BindableBase
    {
        public double BoardHeight { get; set; }
        public double BoardWidth { get; set; }

        private GameConfiguration currentConfig = Settings.DefaultGameConfig;
        
        private IDrawable _canvas;
        public IDrawable Canvas 
        { 
            get => _canvas; 
            set
            {
                _canvas = value;
                RaisePropertyChanged();
            } 
        }

        private GameCellViewModel[,] _gameCellViewModels;
        private GameBoard _gameBoard { get; set; }

        public GameBoardViewModel(IEventAggregator eventAggregator, IDrawable visualHost)
        {
            eventAggregator.GetEvent<RestartGameEvent>().Subscribe(Restart);
            eventAggregator.GetEvent<ChangeGameConfigEvent>().Subscribe(ChangeWorld);
            
            Canvas = visualHost;
            
            ChangeWorld(Settings.DefaultGameConfig);
            InitializeCells();
            Invalidate();
        }

        private void ChangeWorld(GameConfiguration config)
        {
            currentConfig = config;
            Restart();
        }

        private void InitializeCells()
        {
            _gameCellViewModels = new GameCellViewModel[_gameBoard.Cols, _gameBoard.Rows];
            
            // Start TODO
            
            BoardHeight = Settings.CellSize.Height * _gameBoard.Cols;
            BoardWidth = Settings.CellSize.Width * _gameBoard.Rows;
            
            RaisePropertyChanged("BoardHeight");
            RaisePropertyChanged("BoardWidth");

            // End TODO
            
            for (int x = 0; x < _gameBoard.Cols; x++)
            {
                for (int y = 0; y < _gameBoard.Rows; y++)
                {
                    var cell = _gameBoard.GetCellByCoord(x, y);
                    var controller = new GameCellViewModel(cell, Settings.CellSize);
                    _gameCellViewModels[(int)cell.Coordinates.X, (int)cell.Coordinates.Y] = controller;
                }
            }
        }

        public void Invalidate() => Canvas.Invalidate(_gameCellViewModels);
        
        private void Restart()
        {
            _gameBoard = new GameBoard(currentConfig);
            InitializeCells();
            Invalidate();
        }
        
        public void HandleCellClick(Point point) 
        {
            var x = (int) Math.Floor(point.X / Settings.CellSize.Width);
            var y = (int) Math.Floor(point.Y / Settings.CellSize.Height);
            
            var cell = _gameBoard.GetCellByCoord(x, y);
            if (cell == null)
                return;

            switch (cell.Type)
            {
                case CellType.Free:
                    _gameBoard.Reveal(x, y);
                    break;
                case CellType.Neighboor:
                    cell.Reveal();
                    break;
                case CellType.Mine:
                    _gameBoard.ForEach(c => c.Reveal());
                    break;
            }
        }
    }
}