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
        public bool IsAttacking => isAttacking;
        
        [SerializeField] protected float attackRange;
        [SerializeField] protected float attackRate;
        [SerializeField] protected float damage;

        protected bool isAttacking;
        protected bool canAttack;
        private float attackWaitTime;
        private float currentTimer;
        protected Animator animator;

        private void Awake()
        {
            attackWaitTime = 1f / attackRate;
            animator = GetComponentInChildren<Animator>();
        }

        private void Update()
        {
            AttackCountdown();
        }

        private void AttackCountdown()
        {
            if (currentTimer >= attackWaitTime)
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
            currentTimer = 0;
        }
        
        public abstract void Attack();
    }
}