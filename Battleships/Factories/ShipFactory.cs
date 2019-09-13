using System;
using System.Collections.Generic;
using System.Linq;
using Battleships.Models;

namespace Battleships.Factories
{
    public class ShipFactory
    {
        private readonly Random _rnd = new Random(Guid.NewGuid().GetHashCode());

        public Ship CreateRandomShip(int segments, ICollection<Ship> existingShips, int boardCols, int boardRows)
        {
            var newShip = CreateRandomShip(segments, boardCols, boardRows);

            //brute-force generating ship not colliding with any of existing ones
            if (existingShips.Any(existingShip => existingShip.Collides(newShip)))
            {
                return CreateRandomShip(segments, existingShips, boardCols, boardRows);
            }

            return newShip;
        }

        private Ship CreateRandomShip(int segments, int boardCols, int boardRows)
        {
            var tmpSegments = segments;

            var shipX = _rnd.Next(0, boardCols); //inclusive, exclusive
            var shipY = _rnd.Next(0, boardRows);

            var buildsOnXAxis = _rnd.NextDouble() >= 0.5;
            var buildDirection = _rnd.Next(0, 2);
            buildDirection = buildDirection == 0 ? -1 : buildDirection; //converts 0 to -1 so i have direction equals to -1 or 1

            var shipSegments = new List<(int X, int Y)> {(shipX, shipY)};
            while (--tmpSegments > 0) //adding next segment according to randomized build Axis and direction
            {
                var segX = buildsOnXAxis ? shipX + buildDirection : shipX;
                var segY = !buildsOnXAxis ? shipY + buildDirection : shipY;
                shipSegments.Add((segX, segY));

                shipX = segX;
                shipY = segY;
            }

            //brute-force generating ship in bounds
            if (shipSegments.Any(seg => seg.X < 0 || seg.X >= boardCols) ||
                shipSegments.Any(seg => seg.Y < 0 || seg.Y >= boardRows))
            {
                return CreateRandomShip(segments, boardCols, boardRows);
            }

            return new Ship(shipSegments);
        }
    }
}
