using Battleships.CellStateStylePersisters;
using Blazor.Extensions.Canvas.Canvas2D;
using System.Collections.Generic;

namespace Battleships.Drawing
{
    public class DrawingContext
    {
        public DrawingContext(Canvas2DContext canvas, IEnumerable<ICellStateStylePersister> cellStylePersisters)
        {
            Canvas = canvas;
            CellStylePersisters = cellStylePersisters;
        }

        public Canvas2DContext Canvas { get; private set; }
        public IEnumerable<ICellStateStylePersister> CellStylePersisters { get; private set; }
    }
}
