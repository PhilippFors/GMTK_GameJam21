using System.Collections.Generic;
using Attachments;
using Attachments.DamageAttachments;
using Entities.Enemy;
using ObjectPool;
using UnityEngine;

namespace TheGun.Bullets
{
    public class BulletBase : MonoBehaviour
    {
        [SerializeField] private float bulletSpeed;
        private float damage;
        private float maxExistTime = 2f;
        private float currentExistTime;

        private List<DamageAttachment> damageAttachments
        {
            get
            {
                var l = new List<DamageAttachment>();
                l.Add(magazineAttachment);
                l.Add(muzzleAttachment);
                return l;
            }
        }

        private MagazineAttachment magazineAttachment;
        private MuzzleAttachment muzzleAttachment;
        private StatusEffectAttachment statusEffectAttachment;
        
        private void Update()
        {
            MoveBullet();
            if (currentExistTime >= maxExistTime)
            {
                DestroyBullet();
            }
            else
            {
                currentExistTime += Time.deltaTime;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Enemy")
            {
                var enemy = other.GetComponent<EnemyBase>();
                var dmg = damage * CalculateDamageMultiplier(enemy);
                Debug.Log($"Damage: {dmg}");
                enemy.TakeDamage(dmg);
            }
            
            DestroyBullet();
        }

        private float CalculateDamageMultiplier(EnemyBase enemy)
        {
            float tempMultiplier = 0;
            
            foreach (DamageAttachment att in damageAttachments)
            {
                if (enemy.DamageType == att.DamageType)
                {
                    tempMultiplier += att.DmgMultiplier;
                }
            }
            
            Debug.Log($" Multiplier: {tempMultiplier}");
            return tempMultiplier;
        }

        private void ApplyStatusEffect(EnemyBase enemy)
        {
            if (statusEffectAttachment.DamageType == enemy.DamageType)
            {
                statusEffectAttachment.ApplyEffect(enemy);
            }
        }
        
        public void Initialize(Vector3 forward, float dmg, StatusEffectAttachment statusEffectAttachment, MuzzleAttachment muzzleAttachment, MagazineAttachment magazineAttachment)
        {
            damage = dmg;
            this.statusEffectAttachment = statusEffectAttachment;
            this.muzzleAttachment = muzzleAttachment;
            this.magazineAttachment = magazineAttachment;
            transform.forward = forward;
        }
        
        private void MoveBullet()
        {
            transform.position += transform.forward * bulletSpeed * Time.deltaTime;
        }

        private void DestroyBullet()
        {
            currentExistTime = 0;
            BulletBasePool.Instance.ReleaseObject(this);
        }
    }
}