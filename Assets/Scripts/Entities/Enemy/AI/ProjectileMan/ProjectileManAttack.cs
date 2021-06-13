using System.Collections;
using ObjectPool;
using UnityEngine;

namespace Entities.Enemy.AI.ProjectileMan
{
    public class ProjectileManAttack : EnemyAttack
    {
        public float BulletSpeed => bulletSpeed;
        [SerializeField] private Transform bulletSpawnPoint;
        [SerializeField] private float bulletSpeed;
        [SerializeField] private int burstAmount;
        [SerializeField] private float burstFireTimer;
        [SerializeField] private bool isShotgun;
        [SerializeField] private float verticalShotgunSpread;
        [SerializeField] private float horizontalShotgunSpread;
        
        public override void Attack()
        {
            if (canAttack)
            {
                canAttack = false;
                if (isShotgun)
                {
                    ShotGunSpread();
                }
                else
                {
                    StartCoroutine(BurstFire());
                }
              
                ResetTimer();
            }
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
            bullet.Initialize(bulletSpawnPoint.forward, damage, bulletSpeed, true);
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
