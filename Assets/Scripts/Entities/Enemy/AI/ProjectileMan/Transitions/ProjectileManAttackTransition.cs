using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Entities.Enemy.AI.ProjectileMan.Transitions
{
    [CreateAssetMenu(menuName = "AI/Projectile Man/Transitions/Attack transition")]
    public class ProjectileManAttackTransition : Transition
    {
        public override bool Check(StateMachine stateMachine)
        {
            var att = (ProjectileManAttack) stateMachine.EnemyAttack;
            
            if (att.TimeToAttack)
            {
                
                AttackTimer(stateMachine, att);
            }
            else
            {
               
                ChaseTimer(stateMachine, att);
            }

            return att.TimeToAttack;
        }

        private void ChaseTimer(StateMachine stateMachine, ProjectileManAttack att)
        {
            if (att.CurrentTimer >= att.RandomChaseTime)
            {
                att.TimeToAttack = true;
                att.CurrentTimer = 0;
                att.RandomChaseTime = att.ChaseTime + Random.Range(-1f, 3f);
            }
            else
            {
                att.CurrentTimer += Time.deltaTime;
            }
        }

        private void AttackTimer(StateMachine stateMachine, ProjectileManAttack att)
        {
            if (att.CurrentTimer >= att.RandomAttackTime)
            {
                att.TimeToAttack = false;
                att.CurrentTimer = 0;
                att.RandomAttackTime = att.AttackTime + Random.Range(-1f, 2.5f);
            }
            else
            {
                att.CurrentTimer += Time.deltaTime;
            }
        }
    }
}