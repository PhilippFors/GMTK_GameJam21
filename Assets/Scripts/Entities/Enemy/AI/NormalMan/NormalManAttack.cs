using System.Collections;
using System.Collections.Generic;
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

    public override void SlowEffect(float strength, float time)
    {
        var enemyBase = GetComponent<EnemyBase>();
        if (!initOldValue)
        {
            oldValue = enemyBase.MovementSpeed;
            initOldValue = true;
        }

        enemyBase.MovementSpeed = oldValue;
        if (effects.TryGetValue("Slow", out var value))
        {
            StopCoroutine(value);
            effects.Remove("Slow");
            enemyBase.MovementSpeed = oldValue;
        }

        var coroutine = StartCoroutine(Slow(strength, time));
        effects.Add("Slow", coroutine);
    }

    private IEnumerator Slow(float strength, float time)
    {
        var enemyBase = GetComponent<EnemyBase>();
        var oldRate = enemyBase.MovementSpeed;

        enemyBase.MovementSpeed = oldRate * (1 - strength);

        yield return new WaitForSeconds(time);

        enemyBase.MovementSpeed = oldRate;
    }

    public override void DamageOverTime(float strength, float time)
    {
        if (!effects.ContainsKey("DoT"))
        {
            var coroutine = StartCoroutine(DoT(strength, time));
            effects.Add("DoT", coroutine);
        }
    }

    private IEnumerator DoT(float strength, float time)
    {
        var coroutine = StartCoroutine(DoDamageOverTime(strength));
        yield return new WaitForSeconds(time);
        StopCoroutine(coroutine);
        effects.Remove("DoT");
    }

    private IEnumerator DoDamageOverTime(float strength)
    {
        while (true)
        {
            GetComponent<EnemyBase>().TakeDamage(strength);
            yield return new WaitForSeconds(1f);
        }
    }

    public override void ExplodingEnemy(float strength, float time)
    {
        explodingStrength = strength;
        if (!effects.ContainsKey("Exploding"))
        {
            var coroutine = StartCoroutine(CanExplodeTimer(time));
            effects.Add("Exploding", coroutine);
        }
    }

    public override void Knockback(float strength)
    {
        throw new System.NotImplementedException();
    }

    private IEnumerator CanExplodeTimer(float time)
    {
        canExplodeOnDeath = true;
        yield return new WaitForSeconds(time);
        canExplodeOnDeath = false;
        effects.Remove("Exploding");
    }
}