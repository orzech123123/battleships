using Battleships.Models;

namespace Battleships.CellStateStylePersisters
{
    public class SunkCellStylePersister : ICellStateStylePersister
    {
        public CellState State => CellState.Sunk;

        public string Style => "yellow";
    }
}
