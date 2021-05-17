using System;
using System.Drawing;

namespace Fourth_wall.Game_Objects
{
    public class DestructibleObject : GameObject
    {
        private int _hp;
        public readonly Size Collider;
        public readonly Image Texture;

        #region Constructor
        public DestructibleObject(Point location, int hp, Size collider, Image texture) : base(location)
        {
            _hp = hp;
            Collider = collider;
            Texture = texture;
        }
        
        public DestructibleObject(Point location, int hp, int width, int height, Image texture) : base(location)
        {
            _hp = hp;
            Texture = texture;
            Collider = new Size(width, height);
        }
        #endregion
        
        public void HPChange(int hp)
        {
            _hp -= hp;
            if (_hp <= 0)
                Die();
        }

        private void Die()
        {
            throw new NotImplementedException();
        }
    }
}