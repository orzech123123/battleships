using Battleships.Models;

namespace Battleships.CellStateStyleProviders
{
    public class HitCellStyleProvider : ICellStateStyleProvider
    {
        public CellState State => CellState.Hit;

        public string Style => "orange";
    }
}
