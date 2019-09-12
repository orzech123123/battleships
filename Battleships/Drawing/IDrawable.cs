using System.Threading.Tasks;

namespace Battleships.Drawing
{
    public interface IDrawable
    {
        Task DrawAsync(DrawingContext context);
    }
}
