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
        private float baseMultiplier = 0.2f;

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

            if (!other.GetComponent<BulletBase>())
            {
                DestroyBullet();
            }
        }

        private float CalculateDamageMultiplier(EnemyBase enemy)
        {
            float tempMultiplier = baseMultiplier;
            var resistances = enemy.DamageResistances;

            foreach (DamageAttachment att in damageAttachments)
            {
                tempMultiplier += att.DmgMultiplier;
            }

            foreach (DamageAttachment att in damageAttachments)
            {
                if (enemy.DamageType != att.DamageType)
                {
                    tempMultiplier *= 1 - resistances.Find(x => x.DamageType == att.DamageType).Resistance;
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

        public void Initialize(Vector3 forward, float dmg, StatusEffectAttachment statusEffectAttachment,
            MuzzleAttachment muzzleAttachment, MagazineAttachment magazineAttachment)
        {
            damage = dmg;
            bulletSpeed = muzzleAttachment.BulletSpeed;
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