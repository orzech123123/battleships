using Battleships.Models;

namespace Battleships.CellStateStyleProviders
{
    public class SeaCellStyleProvider : ICellStateStyleProvider
    {
        public CellState State => CellState.Sea;

        public string Style => "#3581a9";
    }
}
