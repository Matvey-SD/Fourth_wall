using System;
using System.Collections.Generic;
using System.Drawing;

namespace Fourth_wall.Game_Objects
{
    public class Hero : GameObject
    {
        private int _maxHp;
        private int _hp;
        private int _damage;
        private int _damageBoost;
        private int _range;
        public readonly List<Image> Texture;
        
        #region Constructor
        public Hero(Point location, int hp, List<Image> texture, int damage, int range) : base(location)
        {
            _hp = hp;
            Texture = texture;
            _damage = damage;
            _range = range;
            _damageBoost = 0;
            _maxHp = 6;
        }

        public Hero(int x, int y, int hp, List<Image> texture, int damage, int range) : base(x, y)
        {
            _hp = hp;
            Texture = texture;
            _damage = damage;
            _range = range;
            _damageBoost = 0;
            _maxHp = 6;
        }
        #endregion

        public int Hp => _hp;

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

        public void HpRemove()
        {
            if (--_hp <= 0)
                Die();
            
        }

        public void AddHp()
        {
            if (_hp < _maxHp)
                _hp++;
        }
        private void Die()
        {
            throw new NotImplementedException();
        }

        private int FullDamage()
        {
            return _damage + _damageBoost;
        }
    }
}