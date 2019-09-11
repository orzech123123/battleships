using Battleships.CellStateStyleProviders;
using Blazor.Extensions.Canvas.Canvas2D;
using System.Collections.Generic;

namespace Battleships.Drawing
{
    public class DrawingContext
    {
        public DrawingContext(Canvas2DContext canvas, IEnumerable<ICellStateStyleProvider> cellStyleProviders)
        {
            Canvas = canvas;
            CellStyleProviders = cellStyleProviders;
        }

        public Canvas2DContext Canvas { get; private set; }
        public IEnumerable<ICellStateStyleProvider> CellStyleProviders { get; private set; }
    }
}
