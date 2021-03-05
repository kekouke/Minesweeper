﻿using System;
using System.Windows;
using System.Windows.Input;
using Minesweeper.Enums;
using Minesweeper.Events;
using Minesweeper.Models;
using Minesweeper.Services;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;

namespace Minesweeper.ViewModels
{
    public class GameBoardViewModel : BindableBase
    {
        private VisualHost _canvas;
        public VisualHost Canvas 
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
        
        private Size cellSize;
        
        private Point ViewPort { get; }

        // TODO: Make interface instead of VisualHost
        public GameBoardViewModel(IEventAggregator eventAggregator)
        {
            eventAggregator.GetEvent<RestartGameEvent>().Subscribe(Restart);
            
            // TODO Start

            var _configService = new GameConfigurationService();
            _gameBoard = new GameBoard(_configService.GetAdvancedConfig);

            ViewPort = new Point(435, 411);
            cellSize = new Size(ViewPort.X / _gameBoard.Cols,
                ViewPort.Y / _gameBoard.Rows);
            
            InitializeCells();

            // TODO End

            Canvas = new VisualHost(); //canvas;

            Invalidate();
        }

        private void InitializeCells()
        {
            _gameCellViewModels = new GameCellViewModel[_gameBoard.Cols, _gameBoard.Rows];

            for (int x = 0; x < _gameBoard.Cols; x++)
            {
                for (int y = 0; y < _gameBoard.Rows; y++)
                {
                    var cell = _gameBoard.GetCellByCoord(x, y);
                    var controller = new GameCellViewModel(cell, cellSize);
                    _gameCellViewModels[(int)cell.Coordinates.X, (int)cell.Coordinates.Y] = controller;
                }
            }
        }

        public void Invalidate() => Canvas.Invalidate(_gameCellViewModels);
        
        private void Restart()
        {
            // TODO Start

            var _configService = new GameConfigurationService();
            _gameBoard = new GameBoard(_configService.GetAdvancedConfig);
            
            InitializeCells();
            
            // TODO End
            
            Invalidate();
        }
        
        public void HandleCellClick(Point point) 
        {
            var x = (int) Math.Floor(point.X / cellSize.Width);
            var y = (int) Math.Floor(point.Y / cellSize.Height);
            
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

        public void Initialize(object visualHost)
        {
            if (visualHost != null)
            {
                Canvas = visualHost as VisualHost;
            }
        }
    }
}