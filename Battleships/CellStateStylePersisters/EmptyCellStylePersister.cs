using Battleships.Models;

namespace Battleships.CellStateStylePersisters
{
    public class EmptyCellStylePersister : ICellStateStylePersister
    {
        public CellState State => CellState.Empty;

        public string Style => "gray";
    }
}
