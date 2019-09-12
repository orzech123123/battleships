using System.Collections.Generic;
using System.Linq;

namespace Battleships.Models
{
    public class Ship
    {
        public IEnumerable<(int X, int Y, bool IsHit)> Segments { get; }

        public Ship(IEnumerable<(int X, int Y)> segments)
        {
            Segments = segments.Select(cell => (cell.X, cell.Y, false));
        }

        public bool IsDestroyed => Segments.All(seg => seg.IsHit);

        public bool Collides(Ship other)
        {
            return Segments.Any(seg => other.Segments.Contains(seg));
        }
    }
}
