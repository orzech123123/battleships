using Battleships.Models;

namespace Battleships.CellStateStyleProviders
{
    public class MishitCellStyleProvider : ICellStateStyleProvider
    {
        public CellState State => CellState.Mishit;

        public string Style => "#69a7c7";
    }
}
