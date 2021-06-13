using System;
using Entities.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Entities.Enemy
{
    public abstract class EnemyAttack : MonoBehaviour
    {
        public float Damage => damage;
        public float AttackRange => attackRange;

        public float AttackRate
        {
            get => attackRate;
            set
            {
                attackRate = value;
                attackWaitTime = 1f / attackRate;
            }
        }
    

        public bool IsAttacking => isAttacking;
        public bool CanExplodeOnDeath => canExplodeOnDeath;
        public float ExplodingStrength => explodingStrength;
        [SerializeField] protected float attackRange;
        [SerializeField] private float attackRate;
        [SerializeField] protected float damage;

        protected bool canExplodeOnDeath;
        protected float explodingStrength;
        protected bool isAttacking;
        protected bool canAttack;
        private float attackWaitTime;
        private float currentTimer;
        protected Animator animator;
        
        protected float oldValue;
        protected bool initOldValue;
        protected Dictionary<string, Coroutine> effects = new Dictionary<string, Coroutine>();

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

        public abstract void SlowEffect(float strength, float time);

        public abstract void DamageOverTime(float strength, float time);

        public abstract void ExplodingEnemy(float strength, float time);

        public abstract void Knockback(float strength);
    }
}