using System.Drawing;

namespace Fourth_wall.Game_Objects
{
    public class Chest : GameObject
    {
        #region Constructor
        public Chest(Point location) : base(location)
        {
        }

        public Chest(int x, int y) : base(x, y)
        {
        }
        #endregion
    }
}