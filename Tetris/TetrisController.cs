
using System;
using System.Timers;
using TetrisSFML.Tetris.Tetraminos;

namespace TetrisSFML.Tetris
{
    public class TetrisController : IDisposable
    {
        public Grid Grid { get; }
        public Tetramino Tetramino { get; private set; }

        private readonly Timer _timer = new(1000);

        public TetrisController(Grid grid)
        {
            Grid = grid;
            Tetramino = TetraminoCreator.GetTetramino(Grid);
            //_timer.Elapsed += (_, _) => Down(false);
        }

        public void Start()
        {
            //_timer.Start();
        }

        public void Stop()
        {
            _timer.Stop();
        }

        public void Down()
        {
            Down(true);
        }

        public void Rotate()
        {
            Tetramino.Rotate();
        }

        public void Move(Shift shift)
        {
            Tetramino.Move(shift);
        }

        private void Down(bool keyDown)
        {
            if (keyDown)
            {
                //_timer.Stop();
                _timer.Start();
            }

            if (!Tetramino.TryDown())
            {
                Tetramino = TetraminoCreator.GetTetramino(Grid);
            }
        }

        public void Dispose()
        {
            _timer.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
