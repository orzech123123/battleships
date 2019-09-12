using System;
using System.Threading.Tasks;
using Battleships.Drawing;

namespace Battleships.Models
{
    public class Board : IDrawable
    {
        private readonly long _cellWidth;
        private readonly long _cellHeight;
        private readonly int _cols;
        private readonly int _rows;
        public Cell[,] _cells;

        public Board(int cols, int rows, long canvasWidth, long canvasHeight)
        {
            _cols = cols;
            _rows = rows;
            _cellWidth = canvasWidth / _cols;
            _cellHeight = canvasHeight / _rows;

            InitializeCells();
        }

        public async Task DrawAsync(DrawingContext context)
        {
            await IterateThroughCellsAsync(async (int col, int row) =>
            {
                await _cells[col, row].DrawAsync(context);
                await DrawGridAsync(context, col, row);
            });
        }

        private async Task DrawGridAsync(DrawingContext context, int col, int row)
        {
            await context.Canvas.BeginPathAsync();
            await context.Canvas.SetLineWidthAsync(1f);
            await context.Canvas.SetStrokeStyleAsync("black");
            await context.Canvas.RectAsync(col * _cellWidth, row * _cellHeight, _cellWidth, _cellHeight);
            await context.Canvas.StrokeAsync();
        }

        private void InitializeCells()
        {
            _cells = new Cell[_cols, _rows];

            IterateThroughCellsAsync(async (int col, int row) =>
            {
                _cells[col, row] = new Cell(col, row, _cellWidth, _cellHeight);
            });
        }

        private async Task IterateThroughCellsAsync(Func<int, int, Task> action)
        {
            for (var col = 0; col < _cols; col++)
            {
                for (var row = 0; row < _rows; row++)
                {
                    await action(col, row);
                }
            }
        }
    }
}
