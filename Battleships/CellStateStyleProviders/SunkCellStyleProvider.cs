using Battleships.Models;

namespace Battleships.CellStateStyleProviders
{
    public class SunkCellStyleProvider : ICellStateStyleProvider
    {
        public CellState State => CellState.Sunk;

        public string Style => "yellow";
    }
}
