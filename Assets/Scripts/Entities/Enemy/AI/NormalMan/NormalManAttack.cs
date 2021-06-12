using Entities.Enemy;

public class NormalManAttack : MeleeAttack
{
    public override void Attack()
    {
        // TODO: Start animation
        if (canAttack)
        {
            StartCoroutine(StartAttackTiming(10, 30));
        }
    }
}
