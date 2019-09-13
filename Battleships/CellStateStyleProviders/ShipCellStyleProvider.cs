using Battleships.Models;

namespace Battleships.CellStateStyleProviders
{
    public class ShipCellStyleProvider : ICellStateStyleProvider
    {
        public CellState State => CellState.Ship;

        public string Style => "#b8d432";
    }
}
