namespace TetrisSFML.Tetris.Tetraminos
{
    public class SquareTetramino : Tetramino
    {
        public SquareTetramino(byte typeId, Point[] points, Grid grid) : base(typeId, null, points, grid)
        {
        }

        public override void Rotate() { }
    }
}
