using Battleships.Models;

namespace Battleships.CellStateStyleProviders
{
    public class EmptyCellStyleProvider : ICellStateStyleProvider
    {
        public CellState State => CellState.Empty;

        public string Style => "gray";
    }
}
