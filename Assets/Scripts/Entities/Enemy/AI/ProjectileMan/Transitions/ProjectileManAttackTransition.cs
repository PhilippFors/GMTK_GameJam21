using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Entities.Enemy.AI.ProjectileMan.Transitions
{
    [CreateAssetMenu(menuName = "AI/Projectile Man/Transitions/Attack transition")]
    public class ProjectileManAttackTransition : Transition
    {
        [SerializeField] private float chaseTime;
        [SerializeField] private float attackTime;

        private float randomChaseTime;
        private float randomAttackTime;
        private bool timeToAttack = false;
        private float currentTimer;

        private void OnEnable()
        {
            randomChaseTime = chaseTime + Random.Range(-1f, 3f);
            randomAttackTime = attackTime + Random.Range(-0.5f, 2f);
        }

        public override bool Check(StateMachine stateMachine)
        {
            if (timeToAttack)
            {
                AttackTimer(stateMachine);
            }
            else
            {
                ChaseTimer(stateMachine);
            }

            return timeToAttack;
        }

        private void ChaseTimer(StateMachine stateMachine)
        {
            if (currentTimer >= randomChaseTime)
            {
                timeToAttack = true;
                currentTimer = 0;
                randomChaseTime = chaseTime + Random.Range(-1f, 3f);
            }
            else
            {
                currentTimer += Time.deltaTime;
            }
        }

        private void AttackTimer(StateMachine stateMachine)
        {
            if (currentTimer >= randomAttackTime)
            {
                timeToAttack = false;
                currentTimer = 0;
                randomAttackTime = attackTime + Random.Range(-0.5f, 2f);
            }
            else
            {
                currentTimer += Time.deltaTime;
            }
        }
    }
}