using System.Windows;
using System.Windows.Media;

namespace tanmak.Engine
{
    public interface GamePlaneControl
    {
        void SetTransfrom(Transform transfrom);
        void SetTransfromOrigin(Point pt);
    }
}
