using System.Threading.Tasks;
using Battleships.Drawing;
using System.Linq;
using System;
using Battleships.CellStateStyleProviders;

namespace Battleships.Models
{
    public class Cell : IDrawable
    {
        private readonly int _x;
        private readonly int _y;
        private readonly int _width;
        private readonly int _height;

        public Cell(int x, int y, int width, int height)
        {
            _x = x;
            _y = y;
            _width = width;
            _height = height;
        }

        public CellState State { get; set; } = CellState.Empty;

        public async Task DrawAsync(DrawingContext context)
        {
            var styleProvider = context
                .CellStyleProviders
                .SingleOrDefault(provider => provider.State == State);

            if(styleProvider == null)
            {
                throw new InvalidOperationException($"There was no {nameof(ICellStateStyleProvider)} found for state {State}");
            }

            await context.Canvas.SetFillStyleAsync(styleProvider.Style);
            await context.Canvas.FillRectAsync(_x * _width, _y * _height, _width, _height);
        }
    }
}
