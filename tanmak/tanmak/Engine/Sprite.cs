using System.Windows.Media;

namespace tanmak.Engine
{
    public abstract class Sprite
    {
        public abstract void Render(GameObject Parent, DrawingContext dc);
    }
}
