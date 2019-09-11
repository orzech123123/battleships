using Battleships.Models;

namespace Battleships.CellStateStyleProviders
{
    public interface ICellStateStyleProvider
    {
        CellState State { get; }
        string Style { get; } //for very simple presentation
    }
}
