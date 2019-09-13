using System.Collections.Generic;
using System.Linq;

namespace Battleships.Models
{
    public class Ship
    {
        public IList<(int X, int Y, bool IsHit)> Segments { get; }

        public Ship(IEnumerable<(int X, int Y)> segments)
        {
            Segments = segments.Select(cell => (cell.X, cell.Y, false)).ToList();
        }

        public bool IsDestroyed => Segments.All(seg => seg.IsHit);

        public void DamageSegment(int x, int y)
        {
            var segmentToDamage = Segments.Single(seg => seg.X == x && seg.Y == y);
            Segments.Remove(segmentToDamage);
            segmentToDamage.IsHit = true;
            Segments.Add(segmentToDamage);
        }

        public bool Collides(Ship other)
        {
            return Segments.Any(seg => other.Collides(seg.X, seg.Y));
        }

        public bool Collides(int otherX, int otherY)
        {
            return Segments.Any(seg => seg.X == otherX && seg.Y == otherY);
        }
    }
}