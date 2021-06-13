using System.Collections;
using System.Collections.Generic;
using ObjectPool;
using UnityEngine;

namespace Entities.Enemy.AI.ProjectileMan
{
    public class ProjectileManAttack : EnemyAttack
    {
        public float BulletSpeed => bulletSpeed;
        public float RandomChaseTime { get; set; }
        public float RandomAttackTime { get; set; }
        public float CurrentTimer { get; set; }
        public bool TimeToAttack { get; set; }

        public float ChaseTime => chaseTime;
        public float AttackTime => attackTime;
        public Vector3 CurrentPoint => currentPoint;

        [Header("ShotgunAnim")] [SerializeField]
        private AnimationClip reload;

        [SerializeField] private AnimationClip shotgunShoot;

        [Header("BurstAnim")] [SerializeField] private AnimationClip burstShoot;

        [SerializeField] private Transform bulletSpawnPoint;
        [SerializeField] private float bulletSpeed;
        [SerializeField] private int burstAmount;
        [SerializeField] private float burstFireTimer;
        [SerializeField] private bool isShotgun;
        [SerializeField] private float verticalShotgunSpread;
        [SerializeField] private float horizontalShotgunSpread;
        [SerializeField] private float chaseTime;
        [SerializeField] private float attackTime;

        private Coroutine pointTimer;
        private Vector3 currentPoint;


        private void Start()
        {
            RandomChaseTime = chaseTime + Random.Range(-2f, 3f);
            RandomAttackTime = attackTime + Random.Range(-0.5f, 2f);
        }

        public override void Attack()
        {
            if (canAttack)
            {
                canAttack = false;
                if (isShotgun)
                {
                    StartCoroutine(ShotGunShot());
                }
                else
                {
                    StartCoroutine(BurstFire());
                }

                ResetTimer();
            }
        }

        public override void SlowEffect(float strength, float time)
        {
            if (!initOldValue)
            {
                oldValue = AttackRate;
                initOldValue = true;
            }

            AttackRate = oldValue;

            if (effects.TryGetValue("Slow", out var value))
            {
                StopCoroutine(value);
                effects.Remove("Slow");
                AttackRate = oldValue;
            }

            var coroutine = StartCoroutine(Slow(strength, time));
            effects.Add("Slow", coroutine);
        }

        private IEnumerator Slow(float strength, float time)
        {
            var oldRate = AttackRate;

            AttackRate = strength * (1 - strength);

            yield return new WaitForSeconds(time);

            AttackRate = oldRate;
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

        private IEnumerator CanExplodeTimer(float time)
        {
            canExplodeOnDeath = true;
            yield return new WaitForSeconds(time);
            canExplodeOnDeath = false;
            effects.Remove("Exploding");
        }

        public override void Knockback(float strength)
        {
            throw new System.NotImplementedException();
        }

        private IEnumerator PointTimer(Vector3 player, float minDistance, float maxDistance)
        {
            yield return new WaitForSeconds(8f);
            FindPointNearPlayer(player, minDistance, maxDistance);
        }

        public void FindPointNearPlayer(Vector3 player, float minDistance, float maxDistance)
        {
            if (pointTimer != null)
            {
                StopCoroutine(pointTimer);
            }

            var randomXY = Random.insideUnitCircle;

            var randomPos = new Vector3(Random.Range(minDistance, maxDistance) * randomXY.x, 0,
                Random.Range(minDistance, maxDistance) * randomXY.y) + player;

            currentPoint = randomPos;

            pointTimer = StartCoroutine(PointTimer(player, minDistance, maxDistance));
        }

        private IEnumerator BurstFire()
        {
            for (int i = 0; i < burstAmount; i++)
            {
                ShootSingleBullet();

                yield return new WaitForSeconds(burstFireTimer);
            }
        }

        private void ShootSingleBullet()
        {
            var bullet = BulletBasePool.Instance.GetObject();
            bullet.transform.position = bulletSpawnPoint.position;
            bullet.gameObject.SetActive(true);
            animator.Play("Attack_Once");
            bullet.Initialize(bulletSpawnPoint.forward, damage, bulletSpeed, true);
        }

        private IEnumerator ShotGunShot()
        {
            animator.Play("Reload");
            yield return new WaitForSeconds(reload.length);
            animator.Play("Attack_Shotgun");
            ShotGunSpread();
        }

        private void ShotGunSpread()
        {
            var bullets = BulletBasePool.Instance.GetObjects(Random.Range(8, 15));
            foreach (var b in bullets)
            {
                var forward = bulletSpawnPoint.forward;
                var speed = bulletSpeed + Random.Range(-0.5f, 0.5f);

                forward.x += Random.Range(-horizontalShotgunSpread, horizontalShotgunSpread);
                forward.y += Random.Range(-verticalShotgunSpread, verticalShotgunSpread);

                b.transform.position = bulletSpawnPoint.position;
                b.gameObject.SetActive(true);
                b.Initialize(forward, damage, speed, true);
            }
        }
    }
}