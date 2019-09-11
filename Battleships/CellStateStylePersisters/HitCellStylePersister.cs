using Battleships.Models;

namespace Battleships.CellStateStylePersisters
{
    public class HitCellStylePersister : ICellStateStylePersister
    {
        public CellState State => CellState.Hit;

        public string Style => "orange";
    }
}
