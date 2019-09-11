using Blazor.Extensions.Canvas.Canvas2D;
using System.Threading.Tasks;

namespace Battleships.Models
{
    public interface IDrawable
    {
        Task DrawAsync(Canvas2DContext context);
    }
}
