using System;
using System.Collections.Generic;
using System.Drawing;

namespace Fourth_wall.Game_Objects
{
    public class Enemy : GameObject
    {
        private int _hp;
        private readonly int _fov = 1000;
        public readonly IEnumerable<Image> Texture;
        public bool IsTriggered { get; private set; } = false;
        public bool IsDead { get; private set; } = false;

        #region Constructor
        public Enemy(Point location, int hp, IEnumerable<Image> texture) : base(location)
        {
            _hp = hp;
            Texture = texture;
        }

        public Enemy(int x, int y, int hp, IEnumerable<Image> texture) : base(x, y)
        {
            _hp = hp;
            Texture = texture;
        }
        #endregion

        public bool IsHeroDetected(Hero hero)
        {
            if (!IsDead)
            {
                var x = hero.Location.X - Location.X;
                var y = hero.Location.Y - Location.Y;
                if (Math.Sqrt(x * x + y * y) <= _fov) 
                    IsTriggered = true;
            }
            return IsTriggered;
        }
        
        public void HpChange(int hp)
        {
            _hp -= hp;
            if (_hp <= 0)
                Die();
        }

        private void Die() => IsDead = true;

        public void Move(Directions direction)
        {
            switch (direction)
            {
                case Directions.Down:
                    ChangeLocation(0, 1);
                    break;
                case Directions.Up:
                    ChangeLocation(0, -1);
                    break;
                case Directions.Left:
                    ChangeLocation(-1, 0);
                    break;
                case Directions.Right:
                    ChangeLocation(1, 0);
                    break;
            }
        }
    }
}