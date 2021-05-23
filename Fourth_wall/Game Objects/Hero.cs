using System;
using System.Collections.Generic;
using System.Drawing;

namespace Fourth_wall.Game_Objects
{
    public class Hero : GameObject
    {
        private int _maxHp;
        public int Hp { get; private set; }
        private int _damage;
        private int _damageBoost;
        private int _range;
        public readonly IEnumerable<Image> Texture;
        
        #region Constructor
        public Hero(Point location, int hp, IEnumerable<Image> texture, int damage, int range) : base(location)
        {
            Hp = hp;
            Texture = texture;
            _damage = damage;
            _range = range;
            _damageBoost = 0;
            _maxHp = 6;
        }

        public Hero(int x, int y, int hp, IEnumerable<Image> texture, int damage, int range) : base(x, y)
        {
            Hp = hp;
            Texture = texture;
            _damage = damage;
            _range = range;
            _damageBoost = 0;
            _maxHp = 6;
        }
        #endregion

        public void Hit(Location level)
        {
            foreach (var destructObj in level.DestructibleObjects)
            {
                var x = Location.X - destructObj.Location.X;
                var y = Location.Y - destructObj.Location.Y;
                if (Math.Sqrt(x * x + y * y) <= _range) 
                    destructObj.HpChange(FullDamage());
            }

            foreach (var enemy in level.Enemies)
            {
                var x = Location.X - enemy.Location.X;
                var y = Location.Y - enemy.Location.Y;
                if (Math.Sqrt(x * x + y * y) <= _range) 
                    enemy.HpChange(FullDamage());
            }
        }
        
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

        public void HpRemove()
        {
            if (--Hp <= 0)
                Die();
            
        }

        public void AddHp()
        {
            if (Hp < _maxHp)
                Hp++;
        }
        
        private int FullDamage()
        {
            return _damage + _damageBoost;
        }
        private void Die()
        {
            throw new NotImplementedException();
        }
    }
}