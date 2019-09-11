using System.Threading.Tasks;
using Blazor.Extensions.Canvas.Canvas2D;

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

        public async Task DrawAsync(Canvas2DContext context)
        {
            await context.SetFillStyleAsync("red");
            await context.FillRectAsync(_x * _width, _y * _height, _width, _height);
        }
    }
}
