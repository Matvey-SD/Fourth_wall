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
        public readonly List<Image> Texture;
        
        #region Constructor
        public Hero(Point location, int hp, List<Image> texture, int maxHp, int damage) : base(location)
        {
            _hp = hp;
            Texture = texture;
            _maxHp = maxHp;
            _damage = damage;
            _damageBoost = 0;
        }

        public Hero(int x, int y, int hp, List<Image> texture, int maxHp, int damage) : base(x, y)
        {
            _hp = hp;
            Texture = texture;
            _maxHp = maxHp;
            _damage = damage;
            _damageBoost = 0;
        }
        #endregion

        public int Hp => _hp;

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

        public int FullDamage()
        {
            return _damage + _damageBoost;
        }
    }
}