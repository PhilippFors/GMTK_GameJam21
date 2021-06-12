using System;
using Entities.Player;
using System.Collections;
using UnityEngine;

namespace Entities.Enemy
{
    public abstract class EnemyAttack : MonoBehaviour
    {
        public float Damage => damage;
        public float AttackRange => attackRange;
        
        [SerializeField] protected float attackRange;
        [SerializeField] protected float attackRate;
        [SerializeField] protected float damage;
        
        protected bool canAttack;
        private float attackTime;
        private float currentTimer;

        private void Awake()
        {
            attackTime = 1f / attackRate;
        }

        private void Update()
        {
            AttackCountdown();
        }

        private void AttackCountdown()
        {
            if (currentTimer >= attackTime)
            {
                if (!canAttack)
                {
                    canAttack = true;
                }
            }
            else
            {
                currentTimer += Time.deltaTime;
                canAttack = false;
            }
        }

        protected void ResetTimer()
        {
            currentTimer -= attackTime;
        }
        public abstract void Attack();
    }
}