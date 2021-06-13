using Entities.Enemy;
using UnityEngine;

public class NormalManAttack : MeleeAttack
{
    [SerializeField] private AnimationClip attack;

    public override void Attack()
    {
        if (canAttack)
        {
            canAttack = false;
            ResetTimer();
            animator.SetTrigger("attack");
            StartCoroutine(StartAttackTiming(0, 25, attack));
        }
    }
}