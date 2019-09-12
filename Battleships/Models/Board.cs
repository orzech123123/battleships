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
        public int Cols { get; }
        public int Rows { get; }
        private Cell[,] _cells;

        public Board(int cols, int rows, int canvasWidth, int canvasHeight)
        {
            Cols = cols;
            Rows = rows;
            CellWidth = canvasWidth / Cols;
            CellHeight = canvasHeight / Rows;

            InitializeCells();
        }

        public async Task DrawAsync(DrawingContext context)
        {
            //FAST DRAWING - PREVIOUSLY THERE WHERE FIRED "await DrawAsync().." METHODS FOR EACH CELL ONE BY ONE ASYNCHORNOUSLY
            //AND CELLS APPEARED ON CANVAS VERY SLOWLY, ONE BY ONE (WHOLE GRID RENDERED ABOUT 4 SECONDS)
            var drawingTasks = new List<Task>();
            IterateThroughCells((col, row) =>
            {
                drawingTasks.Add(_cells[col, row].DrawAsync(context)); 
                drawingTasks.Add(DrawGridAsync(context, col, row));
            });
            await Task.WhenAll(drawingTasks.ToArray());
        }

        public void Clear()
        {
            InitializeCells();
        }

        public void SetCellState(int col, int row, CellState state)
        {
            _cells[col, row].State = state;
        }

        public CellState GetCellState(int col, int row)
        {
            return _cells[col, row].State;
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
            _cells = new Cell[Cols, Rows];

            IterateThroughCells((col, row) =>
            {
                _cells[col, row] = new Cell(col, row, CellWidth, CellHeight);
            });
        }

        private void IterateThroughCells(Action<int, int> action)
        {
            for (var col = 0; col < Cols; col++)
            {
                for (var row = 0; row < Rows; row++)
                {
                    action(col, row);
                }
            }
        }
    }
}
