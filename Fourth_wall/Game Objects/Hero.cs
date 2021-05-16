using System;
using System.Collections.Generic;
using System.Drawing;

namespace Fourth_wall.Game_Objects
{
    public class Hero : GameObject
    {
        private int _maxHp;
        private int _hp;
        public readonly List<Image> Texture;
        
        #region Constructor
        public Hero(Point location, int hp, List<Image> texture, int maxHp) : base(location)
        {
            _hp = hp;
            Texture = texture;
            _maxHp = maxHp;
        }

        public Hero(int x, int y, int hp, List<Image> texture, int maxHp) : base(x, y)
        {
            _hp = hp;
            Texture = texture;
            _maxHp = maxHp;
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
    }
}