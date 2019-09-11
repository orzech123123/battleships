using Battleships.Models;

namespace Battleships.CellStateStylePersisters
{
    public class ShipCellStylePersister : ICellStateStylePersister
    {
        public CellState State => CellState.Ship;

        public string Style => "green";
    }
}
