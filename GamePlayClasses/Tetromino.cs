using System.Collections.Generic;
using System.Drawing;

namespace TetrisSFML.GamePlayClasses
{
    internal abstract class Tetromino
    {
        IEnumerable<Point> _points;
        public Point Position { get; private set; }

        public void Down()
        {
        }

        public void Shift()
        {
        }

        public abstract void Rotate();
    }
}
