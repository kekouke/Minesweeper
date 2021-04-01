﻿using System;
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
        private double _boardHeight;
        public double BoardHeight
        {
            get => _boardHeight;
            set
            {
                _boardHeight = value;
                RaisePropertyChanged();
            }
        }

        private double _boardWidth;
        public double BoardWidth 
        {
            get => _boardWidth;
            set
            {
                _boardWidth = value;
                RaisePropertyChanged();
            }
        }


        private GameState _gameState;
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
        private IEventAggregator _eventAggregator;
        private GameBoard _gameBoard { get; set; }

        public GameBoardViewModel(IEventAggregator eventAggregator, IDrawable visualHost)
        {
            eventAggregator.GetEvent<RestartGameEvent>().Subscribe(() => StartNewGame(currentConfig));
            eventAggregator.GetEvent<ChangeGameConfigEvent>().Subscribe(StartNewGame);

            _eventAggregator = eventAggregator;
            Canvas = visualHost;

            StartNewGame(Settings.DefaultGameConfig);
        }

        private void StartNewGame(GameConfiguration configuration)
        {
            currentConfig = configuration;

            InitializeCells();
            Invalidate();

            _gameState = GameState.Initial;

            _eventAggregator.GetEvent<StartNewGameEvent>().Publish();
        }

        private void InitializeCells()
        {
            _gameBoard = new GameBoard(currentConfig);
            _gameCellViewModels = new GameCellViewModel[_gameBoard.Cols, _gameBoard.Rows];
            
            BoardHeight = Settings.CellSize.Height * _gameBoard.Cols;
            BoardWidth = Settings.CellSize.Width * _gameBoard.Rows;
            
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

        public void Invalidate() => Canvas.Draw(_gameCellViewModels);
        
        public void HandleCellClick(Point point, int mouseType)
        {
            if (_gameState == GameState.GameOver)
                return;

            if (_gameState == GameState.Initial)
            {
                _gameState = GameState.InProcess;
                _eventAggregator.GetEvent<FirstCellClickEvent>().Publish();
            }
            
            var x = (int) Math.Floor(point.X / Settings.CellSize.Width);
            var y = (int) Math.Floor(point.Y / Settings.CellSize.Height);
            
            var cell = _gameBoard.GetCellByCoord(x, y);
            if (cell == null)
                return;

            if (mouseType == 0) OnLeftClick(cell);
            else OnRightClick(cell);
        }

        private void OnLeftClick(Cell cell)
        {
            if (cell.IsFlagged)
                return;
            
            switch (cell.Type)
            {
                case CellType.Free:
                    _gameBoard.Reveal((int)cell.Coordinates.X, (int)cell.Coordinates.Y);
                    break;
                case CellType.Neighboor:
                    cell.Reveal();
                    break;
                case CellType.Mine:
                    _gameBoard.ForEach(c => c.Reveal());
                    _gameState = GameState.GameOver;
                    _eventAggregator.GetEvent<GameOverEvent>().Publish();
                    break;
            }
        }

        private void OnRightClick(Cell cell)
        {
            if (cell.IsRevealed) return;
            
            cell.SetFlag();

            int count = currentConfig.MinesCount;
            _gameBoard.ForEach(x =>
            {
                if (x.IsFlagged)
                {
                    count--;
                }
            });

            MessageBox.Show(count.ToString()); // TODO
        }
    }
}