using System;
using System.Timers;

namespace TetrisSFML
{
    internal class GamePlay
    {
        public int CompletedLines { get; private set; }
        public Tetromino NextTetromino { get; private set; }

        private Timer _timer;
        private GameGrid _gameGrid;
        private Tetromino _currentTetromino;

        public GamePlay(TimeSpan tickInterval)
        {
            _timer = new Timer(tickInterval.TotalMilliseconds);
            _timer.Elapsed += Tick;
        }

        public void Start()
        {
            _timer.Start();
        }

        public void Pause()
        {
            _timer.Stop();
        }

        public void TetrominoShift(SideShift sideShift)
        {
            throw new NotImplementedException();
        }

        public void TetrominoRotate()
        {
            throw new NotImplementedException();
        }

        public void TetrominoDrop()
        {
            throw new NotImplementedException();
        }

        public void TetrominoDown()
        {
        }

        private void Tick(object sender, ElapsedEventArgs e)
        {

        }

        private Tetromino GetRandomTetromino()
        {
            throw new NotImplementedException();
        }

        private void TetrominoMove()
        {
        }
    }
}
