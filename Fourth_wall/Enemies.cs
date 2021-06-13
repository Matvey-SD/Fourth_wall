using Fourth_wall.Game_Objects;

namespace Fourth_wall
{
    public static class Enemies
    {
        public static Enemy CreateLightEnemy(int x, int y) => new Enemy(x, y, EnemyType.Light);

        public static Enemy CreateHeavyEnemy(int x, int y) => new Enemy(x, y, EnemyType.Heavy);

        public static Enemy CreateBoss(int x, int y) => new Enemy(x, y, EnemyType.Boss);
    }
}