using tanmak.Engine;

namespace tanmak.Game
{
    public abstract class EnemyBullet : GameObject
    {
        public EnemyBullet(World world) : base(world)
        {
        }

        public int Damage { get; set; } = 1;
    }
}
