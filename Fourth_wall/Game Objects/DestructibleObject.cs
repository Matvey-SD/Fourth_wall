using System;
using System.Drawing;

namespace Fourth_wall.Game_Objects
{
    public class DestructibleObject : GameObject
    {
        private int _hp;
        public readonly Size Collider;
        public readonly Image Texture;
        private Point OppositeCorner => new Point(Location.X + Collider.Width, Location.Y + Collider.Height);
        public Point MiddlePoint => new Point(Location.X + Collider.Width / 2, Location.Y + Collider.Height / 2);
        public bool IsDestroyed { get; private set; } = false;

        #region Constructor
        public DestructibleObject(Point location, Size collider, int hp, Image texture) : base(location)
        {
            _hp = hp;
            Collider = collider;
            Texture = texture;
        }
        
        public DestructibleObject(Point location, int width, int height, int hp, Image texture) : base(location)
        {
            _hp = hp;
            Texture = texture;
            Collider = new Size(width, height);
        }
        
        public DestructibleObject(int x, int y, Size collider, int hp, Image texture) : base(x, y)
        {
            _hp = hp;
            Collider = collider;
            Texture = texture;
        }
        
        public DestructibleObject(int x, int y, int width, int height, int hp, Image texture) : base(x, y)
        {
            _hp = hp;
            Texture = texture;
            Collider = new Size(width, height);
        }
        #endregion

        public bool IsPointInside(Point point)
        {
            return (point.X > Location.X && point.X < OppositeCorner.X
                                         && point.Y > Location.Y && point.Y < OppositeCorner.Y);
        }
        
        public void HpChange(int hp)
        {
            _hp -= hp;
            if (_hp <= 0)
                Die();
        }

        private void Die()
        {
            IsDestroyed = true;
        }
    }
}