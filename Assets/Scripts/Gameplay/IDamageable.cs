
public interface IDamageable
{
    public bool IsDead { get; }
    public void Damage(int damage);
}