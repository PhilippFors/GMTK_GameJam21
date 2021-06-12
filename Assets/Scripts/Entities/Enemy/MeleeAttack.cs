using System.Collections;
using Entities.Player;
using UnityEngine;

namespace Entities.Enemy
{
    public abstract class MeleeAttack : EnemyAttack
    {
        [SerializeField] private HitDetector attackCollider;
        
        private Coroutine damageWaiter;
        private Coroutine attackTimingCoroutine;
        
        private bool canDamage;

        // protected bool DoDamage()
        // {
        //     if (canDamage & attackCollider.Player != null)
        //     {
        //         attackCollider.Player.GetComponent<PlayerBase>().TakeDamage(Damage);
        //         Debug.Log($"Player recieves {Damage} damage");
        //         return true;
        //     }
        //
        //     return false;
        // }
        
        protected IEnumerator StartAttackTiming(float startDamageFrame, float stopDamageFrame, AnimationClip clip, float clipLength = 0)
        {
            isAttacking = true;
            
            // float start = 0;
            // if (startDamageFrame != 0)
            // {
            //     start = startDamageFrame / 24;
            // }
            //
            // float end = (stopDamageFrame - startDamageFrame) / 24;
            //
            // yield return new WaitForSeconds(start);
            //
            // canDamage = true;
            //
            // damageWaiter = StartCoroutine(DamageTimer(end));
            // yield return damageWaiter;
            //
            // canDamage = false;
            //
            
            yield return new WaitForSeconds(clip.length);
            
            isAttacking = false;
        }

        // protected IEnumerator DamageTimer(float wait)
        // {
        //     float cTime = 0;
        //     while (cTime <= wait)
        //     {
        //         if (DoDamage())
        //         {
        //             yield break;
        //         }
        //
        //         cTime += Time.deltaTime;
        //         yield return null;
        //     }
        // }

        public override void Attack(){}
    }
}