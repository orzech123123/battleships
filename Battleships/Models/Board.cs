using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Battleships.Drawing;

namespace Battleships.Models
{
    public class Board : IDrawable
    {
        public int CellWidth { get; }
        public int CellHeight { get; }
        private readonly int _cols;
        private readonly int _rows;
        private Cell[,] _cells;

        public Board(int cols, int rows, int canvasWidth, int canvasHeight)
        {
            _cols = cols;
            _rows = rows;
            CellWidth = canvasWidth / _cols;
            CellHeight = canvasHeight / _rows;

            InitializeCells();
        }

        public async Task DrawAsync(DrawingContext context)
        {
            //FAST DRAWING - PREVIOUSLY THERE WHERE FIRED "await DrawAsync().." METHODS FOR EACH CELL ONE BY ONE ASYNCHORNOUSLY
            //AND CELLS APPEARED ON CANVAS VERY SLOWLY, ONE BY ONE (WHOLE GRID RENDERED ABOUT 4 SECONDS)
            var drawingTasks = new List<Task>();
            IterateThroughCellsAsync((int col, int row) =>
            {
                drawingTasks.Add(_cells[col, row].DrawAsync(context));
                drawingTasks.Add(DrawGridAsync(context, col, row));
            });
            await Task.WhenAll(drawingTasks.ToArray());
        }

        public void SetCellState(int col, int row, CellState state)
        {
            _cells[col, row].State = state;
        }

        private async Task DrawGridAsync(DrawingContext context, int col, int row)
        {
            await context.Canvas.BeginPathAsync();
            await context.Canvas.SetLineWidthAsync(1f);
            await context.Canvas.SetStrokeStyleAsync("black");
            await context.Canvas.RectAsync(col * CellWidth, row * CellHeight, CellWidth, CellHeight);
            await context.Canvas.StrokeAsync();
        }

        private void InitializeCells()
        {
            _cells = new Cell[_cols, _rows];

            IterateThroughCellsAsync((int col, int row) =>
            {
                _cells[col, row] = new Cell(col, row, CellWidth, CellHeight);
            });
        }

        private void IterateThroughCellsAsync(Action<int, int> action)
        {
            for (var col = 0; col < _cols; col++)
            {
                for (var row = 0; row < _rows; row++)
                {
                    action(col, row);
                }
            }
        }
    }
}
