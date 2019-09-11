using System;
using System.Threading.Tasks;
using Blazor.Extensions.Canvas.Canvas2D;

namespace Battleships.Models
{
    public class Board : IDrawable
    {
        private readonly int _cols;
        private readonly int _rows;
        public Cell[,] _cells;

        public Board(int cols, int rows)
        {
            _cols = cols;
            _rows = rows;

            _cells = new Cell[cols, rows];
        }

        public async Task DrawAsync(Canvas2DContext context)
        {
            await IterateThroughCellsAsync(async (int col, int row) =>
            {
                await _cells[col, row].DrawAsync(context);
            });
        }

        public async Task InitializeAsync(long canvasWidth, long canvasHeight)
        {
            var cellWidth = canvasWidth / _cols;
            var cellHeight = canvasHeight / _rows;

            await IterateThroughCellsAsync(async (int col, int row) =>
            {
                _cells[col, row] = new Cell(col, row, cellWidth, cellHeight);
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
