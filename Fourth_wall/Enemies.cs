using System.Collections.Generic;
using System.Drawing;
using Fourth_wall.Game_Objects;

namespace Fourth_wall
{
    public class Enemies
    {
        public static Enemy CreateLightEnemy(int x, int y) => new Enemy(x, y, 14, EnemyType.Light);

        public static Enemy CreateLightEnemy(Point location) => new Enemy(location, 14, EnemyType.Light);
        
        public static Enemy CreateHeavyEnemy(int x, int y) => new Enemy(x, y, 28, EnemyType.Heavy);

        public static Enemy CreateHeavyEnemy(Point location) => new Enemy(location, 28, EnemyType.Heavy);
    }
}