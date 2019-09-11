using Battleships.Models;

namespace Battleships.CellStateStylePersisters
{
    public interface ICellStateStylePersister
    {
        CellState State { get; }
        string Style { get; } //for very simple presentation
    }
}
