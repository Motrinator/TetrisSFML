﻿using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace TetrisSFML.Tetris
{
    internal class TetrisViewModel : IDisposable
    {
        private readonly TetrisView _view;
        private readonly TetrisController _tetrisController;

        public TetrisViewModel()
        {
            _view = new TetrisView();
            _tetrisController = new TetrisController(
                new Grid((x, y) => { }, (x, y) => { }, (x) => { }, () => { })
            );
        }

        public void Run()
        {
            _view.RenderWindow.KeyReleased += KeyEventHandler;
            _tetrisController.Start();

            while (_view.RenderWindow.IsOpen)
{
                _view.RenderWindow.DispatchEvents();
                _view.Draw(_tetrisController.Tetramino.TypeId, _tetrisController.Tetramino.PointPositions, _tetrisController.Grid.Area);
            }
        }

        private void KeyEventHandler(object? sender, KeyEventArgs keyEventArgs)
        {
            switch (keyEventArgs.Code)
            {
                case Keyboard.Key.Down:
                case Keyboard.Key.S:
                    _tetrisController.Down();
                    break;
                case Keyboard.Key.Up:
                case Keyboard.Key.W:
                    _tetrisController.Rotate();
                    break;
                case Keyboard.Key.Left:
                case Keyboard.Key.A:
                    _tetrisController.Move(Shift.Left);
                    break;
                case Keyboard.Key.Right:
                case Keyboard.Key.D:
                    _tetrisController.Move(Shift.Right);
                    break;
            }
        }

        public void Dispose()
        {
            _view.Dispose();
            _tetrisController.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
