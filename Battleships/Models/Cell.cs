using System.Threading.Tasks;
using Battleships.Drawing;

namespace Battleships.Models
{
    public class Cell : IDrawable
    {
        private readonly int _x;
        private readonly int _y;
        private readonly long _width;
        private readonly long _height;

        public Cell(int x, int y, long width, long height)
        {
            _x = x;
            _y = y;
            _width = width;
            _height = height;
        }

        public CellState State { get; set; } = CellState.Empty;

        public async Task DrawAsync(DrawingContext context)
        {
            await context.Canvas.SetFillStyleAsync("red");
            await context.Canvas.FillRectAsync(_x * _width, _y * _height, _width, _height);
        }
    }
}
