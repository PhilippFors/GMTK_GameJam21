using System.Collections;
using Entities.Player;
using UnityEngine;

namespace Entities.Enemy
{
    public abstract class MeleeAttack : EnemyAttack
    {
        [SerializeField] private HitDetector attackCollider;
        
        private Coroutine damageWaiter;
        private Coroutine attackTimer;
        
        private bool canDamage;

        protected bool DoDamage()
        {
            if (canDamage & attackCollider.Player != null)
            {
                attackCollider.Player.GetComponent<PlayerBase>().TakeDamage(Damage);
            }

            return false;
        }
        
        protected IEnumerator StartAttackTiming(float startDamageFrame, float stopDamageFrame, float clipLength = 0)
        {
            float start;
            if (startDamageFrame != 0)
                start = startDamageFrame / 60;
            else
                start = 0;

            float end = (stopDamageFrame - startDamageFrame) / 60;

            yield return new WaitForSeconds(start);

            canDamage = true;

            damageWaiter = StartCoroutine(DamageTimer(end));
            yield return damageWaiter;

            canDamage = false;
        }

        protected IEnumerator DamageTimer(float wait)
        {
            float cTime = 0;
            while (cTime <= wait)
            {
                if (DoDamage())
                    yield break;

                cTime += Time.deltaTime;
                yield return null;
            }
        }

        public abstract void Attack();
    }
}