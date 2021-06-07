using System.Collections.Generic;
using System.Drawing;

namespace Fourth_wall
{
    public interface ICreature
    {
        IEnumerable<Point> ColliderBorders();
        int Speed { get;}
    }
}