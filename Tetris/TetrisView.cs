using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TetrisSFML.Tetris
{
    internal class TetrisView : IDisposable
    {
        private const int _cellSise = 8;
        private const int _screenResize = 4;

        public RenderWindow RenderWindow { get; }

        private readonly RectangleShape _cellTemplate = new(new Vector2f(_cellSise - 1, _cellSise - 1));
        private readonly Color[] _tetraminoColors = new Color[]
        {
            new Color(255, 255, 255),
            new Color(0, 240, 240),
            new Color(0, 0, 240),
            new Color(240, 160, 0),
            new Color(240, 240, 0),
            new Color(0, 240, 0),
            new Color(160, 0, 240),
            new Color(240, 0, 0)
        };

        public TetrisView()
        {
            RenderWindow = new(
                new VideoMode(
                    2 * _cellSise * Grid.Columns * _screenResize,
                    _cellSise * Grid.Rows * _screenResize
                ),
                "Tetris",
                Styles.Close
            );

            RenderWindow.SetView(new View(new FloatRect(0, 0, 2 * _cellSise * Grid.Columns, (_cellSise * Grid.Rows) + 1)));
            RenderWindow.SetFramerateLimit(60);
            RenderWindow.Closed += (_, _) => RenderWindow.Close();
        }

        public void Draw(byte tetraminoType, IEnumerable<Point> points, IReadOnlyList<IReadOnlyList<byte>> matrix)
        {
            if (points is null)
            {
                throw new ArgumentNullException(nameof(points));
            }

            if (matrix is null)
            {
                throw new ArgumentNullException(nameof(matrix));
            }

            RenderWindow.Clear();

            for (int row = 0; row < matrix.Count; row++)
            {
                for (int col = 0; col < matrix[row].Count; col++)
                {
                    _cellTemplate.Position = new Vector2f(_cellSise * col, (_cellSise * row) + 1);
                    _cellTemplate.FillColor = _tetraminoColors[matrix[row][col]];

                    RenderWindow.Draw(_cellTemplate);
                }
            }

            _cellTemplate.FillColor = _tetraminoColors[tetraminoType];

            foreach (Point point in points)
            {
                _cellTemplate.Position = new Vector2f(_cellSise * point.Y, (_cellSise * point.X) + 1);

                RenderWindow.Draw(_cellTemplate);
            }

            RenderWindow.Display();
         }

        public void Dispose()
        {
            RenderWindow.Dispose();
            _cellTemplate.Dispose();
        }
    }
}
