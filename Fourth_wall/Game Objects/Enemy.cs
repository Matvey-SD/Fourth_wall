using System;
using System.Collections.Generic;
using System.Drawing;

namespace Fourth_wall.Game_Objects
{
    public class Enemy : GameObject
    {
        private int _hp;
        private readonly int _fov = 1000;
        public readonly List<Image> Texture;
        public bool _isTriggered = false;

        #region Constructor
        public Enemy(Point location, int hp, List<Image> texture) : base(location)
        {
            _hp = hp;
            Texture = texture;
        }

        public Enemy(int x, int y, int hp, List<Image> texture) : base(x, y)
        {
            _hp = hp;
            Texture = texture;
        }
        #endregion

        public bool IsHeroDetected(Hero hero)
        {
            var x = hero.Location.X - Location.X;
            var y = hero.Location.Y - Location.Y;
            if (Math.Sqrt(x * x + y * y) <= _fov) 
                _isTriggered = true;
            return _isTriggered;
        }
        
        public void HpChange(int hp)
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