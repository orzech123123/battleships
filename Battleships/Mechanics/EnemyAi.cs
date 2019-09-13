using System;
using System.Collections.Generic;
using System.Linq;
using Battleships.Components;
using Battleships.Models;

namespace Battleships.Mechanics
{
    public class EnemyAi : IEnemyAi
    {
        private readonly Random _rnd = new Random(Guid.NewGuid().GetHashCode());

        public (int Col, int Row) GetNextBestShot(BoardComponent boardComponent, ICollection<Ship> ships)
        {
            var damagedShip = ships.FirstOrDefault(ship => ship.IsDamaged && !ship.IsDestroyed);
            if (damagedShip != null)
            {
                return GetNextShotInDamagedShip(boardComponent, damagedShip);
            }

            var (col, row) = (_rnd.Next(0, boardComponent.Board.Cols), _rnd.Next(0, boardComponent.Board.Rows));

            //brute-force
            if (!new[] { CellState.Sea, CellState.Ship }.Contains(boardComponent.Board.GetCellState(col, row)))
            {
                return GetNextBestShot(boardComponent, ships);
            }

            return (col, row);
        }

        private (int Col, int Row) GetNextShotInDamagedShip(BoardComponent boardComponent, Ship damagedShip)
        {
            foreach (var damagedSegment in damagedShip.Segments.Where(seg => seg.IsHit))
            {
                var possibleNextHits = new List<(int col, int row)>();
                for (var xAxis = -1; xAxis <= 1; xAxis++)
                {
                    for (var yAxis = -1; yAxis <= 1; yAxis++)
                    {
                        if (xAxis == 0 && yAxis == 0) continue;

                        var cellX = damagedSegment.X + xAxis;
                        var cellY = damagedSegment.Y + yAxis;

                        if(!boardComponent.Board.IsValidCell(cellX, cellY)) continue;

                        if (!new [] { CellState.Sea, CellState.Ship}.Contains(boardComponent.Board.GetCellState(cellX, cellY))) continue;

                        possibleNextHits.Add((cellX, cellY));
                    }
                }

                if (possibleNextHits.Any())
                {
                    return possibleNextHits.OrderBy(hit => _rnd.Next()).First();
                }
            }

            throw new NotSupportedException("Though there is at least one damage ship I didn't find cell to shot!'");
        }
    }
}
