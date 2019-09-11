using Battleships.Models;

namespace Battleships.CellStateStylePersisters
{
    public class MishitCellStylePersister : ICellStateStylePersister
    {
        public CellState State => CellState.Mishit;

        public string Style => "black";
    }
}
