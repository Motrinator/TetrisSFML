using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TetrisSFML.Tetris.Tetraminos;

namespace TetrisSFML.Tetris
{
    internal static class TetraminoCreator
    {
        private static readonly Random _random = new();
        private static readonly IReadOnlyList<Func<Grid, Tetramino>> _getTetraminoFuncs;

        static TetraminoCreator()
        {
            _getTetraminoFuncs = new Func<Grid, Tetramino>[]
            {
                (x) => new Tetramino(
                    typeId: 1,
                    rotation: (byte)_random.Next(0, 4),
                    points: new Point[]
                    {
                        new Point(0, 2),
                        new Point(1, 2),
                        new Point(2, 2),
                        new Point(3, 2)
                    },
                    grid: x
                ),
                (x) => new Tetramino(
                    typeId: 2,
                    rotation: (byte)_random.Next(0, 4),
                    points: new Point[]
                    {
                        new Point(0, 0),
                        new Point(1, 0),
                        new Point(1, 1),
                        new Point(1, 2)
                    },
                    grid: x
                ),
                (x) => new Tetramino(
                    typeId: 3,
                    rotation: (byte)_random.Next(0, 4),
                    points: new Point[]
                    {
                        new Point(1, 0),
                        new Point(0, 2),
                        new Point(1, 1),
                        new Point(1, 2)
                    },
                    grid: x
                ),
                (x) => new Tetramino(
                    typeId: 4,
                    rotation: null,
                    points: new Point[]
                    {
                        new Point(0, 0),
                        new Point(0, 1),
                        new Point(1, 0),
                        new Point(1, 1)
                    },
                    grid: x
                ),
                (x) => new Tetramino(
                    typeId: 5,
                    rotation: (byte)_random.Next(0, 4),
                    points: new Point[]
                    {
                        new Point(2, 0),
                        new Point(2, 1),
                        new Point(1, 1),
                        new Point(1, 2)
                    },
                    grid: x
                ),
                (x) => new Tetramino(
                    typeId: 6,
                    rotation: (byte)_random.Next(0, 4),
                    points: new Point[]
                    {
                        new Point(0, 1),
                        new Point(1, 0),
                        new Point(1, 1),
                        new Point(1, 2)
                    },
                    grid: x
                ),
                (x) => new Tetramino(
                    typeId: 7,
                    rotation: (byte)_random.Next(0, 4),
                    points: new Point[]
                    {
                        new Point(1, 0),
                        new Point(1, 1),
                        new Point(2, 1),
                        new Point(2, 2)
                    },
                    grid: x
                ),
            };
        }

        public static Tetramino GetTetramino(Grid grid)
        {
            if (grid is null)
            {
                throw new ArgumentNullException(nameof(grid));
            }

            //return _getTetraminoFuncs[0](grid);
            return _getTetraminoFuncs[_random.Next(0, _getTetraminoFuncs.Count)](grid);
        }
    }
}
