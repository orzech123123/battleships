using System;
using Battleships.Components;

namespace Battleships.Mechanics
{
    public class EnemyAi
    {
        private readonly Random _rnd = new Random(Guid.NewGuid().GetHashCode());

        public (int Col, int Row) GetNextBestShot(BoardComponent boardComponent)
        {
            return (_rnd.Next(0, boardComponent.Board.Cols), _rnd.Next(0, boardComponent.Board.Rows));
        }
    }
}
