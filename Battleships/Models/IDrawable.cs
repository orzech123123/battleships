using Battleships.Drawing;
using System.Threading.Tasks;

namespace Battleships.Models
{
    public interface IDrawable
    {
        Task DrawAsync(DrawingContext context);
    }
}
