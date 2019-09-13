using System.Collections.Generic;
using Battleships.Components;
using Battleships.Models;

namespace Battleships.Mechanics
{
    public interface IEnemyAi
    {
        (int Col, int Row) GetNextBestShot(BoardComponent boardComponent, ICollection<Ship> ships);
    }
}
