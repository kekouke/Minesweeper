using System;
using Minesweeper.Commands;
using System.Windows;
using System.Windows.Input;
using Minesweeper.Models;
using Minesweeper.Services;

namespace Minesweeper
{
    public class GameBoardController
    {
        public ICommand RestartGameCommand { get; set; }
        
        private readonly VisualHost Canvas;

        private GameCellController[,] _gameCellControllers;
        private GameBoard _gameBoard { get; set; }
        
        private Point ViewPort { get; }

        // TODO: Make interface instead of VisualHost
        public GameBoardController(VisualHost canvas)
        {
            // TODO Start

            var _configService = new GameConfigurationService();
            _gameBoard = new GameBoard(_configService.GetBeginnerConfig);

            ViewPort = new Point(435, 411);
            
            InitializeCells();
            
            // TODO End

            RestartGameCommand = new ClickCommand(Restart);
            Canvas = canvas;

            Invalidate();
        }

        private void InitializeCells()
        {
            var cellSize = new Size(ViewPort.X / _gameBoard.Cols,
                ViewPort.Y / _gameBoard.Rows);
                
            _gameCellControllers = new GameCellController[_gameBoard.Cols, _gameBoard.Rows];

            for (int x = 0; x < _gameBoard.Cols; x++)
            {
                for (int y = 0; y < _gameBoard.Rows; y++)
                {
                    var cell = _gameBoard.GetCellByCoord(x, y);
                    var controller = new GameCellController(cell, cellSize);
                    _gameCellControllers[(int)cell.Coordinates.X, (int)cell.Coordinates.Y] = controller;
                }
            }
        }

        public void Invalidate() => Canvas.Invalidate(_gameCellControllers);
        
        private void Restart()
        {
            // TODO Start

            var _configService = new GameConfigurationService();
            _gameBoard = new GameBoard(_configService.GetBeginnerConfig);
            
            InitializeCells();
            
            // TODO End
            
            Invalidate();
        }

        public void HandleCellClick(Point point) //TODO (Index out of rabge)
        {
            var cellSize = new Size(ViewPort.X / _gameBoard.Cols,
                ViewPort.Y / _gameBoard.Rows);
            
            int x = (int)Math.Floor(point.X / cellSize.Width);
            int y = (int)Math.Floor(point.Y / cellSize.Height);

            if (_gameBoard.GetCellByCoord(x, y).IsBomb)
            {
                _gameBoard.ForEach(c => c.Reveal());
            }
            else
            {
                // TODO
                _gameBoard.GetCellByCoord(x, y).Reveal(); // Field.BFS(x, y);
                _gameBoard.Reveal(x, y);
            }

            Invalidate();
        }
    }
}
