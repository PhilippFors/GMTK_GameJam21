using System;
using System.Collections.Generic;
using Attachments;
using Attachments.DamageAttachments;
using Effects;
using Entities.Enemy;
using Entities.Player;
using Entities.Player.PlayerInput;
using ObjectPool;
using UnityEngine;

namespace TheGun.Bullets
{
    public class BulletBase : MonoBehaviour
    {
        [SerializeField] private float bulletSpeed;
        private bool fromEnemy;
        private float damage;
        private float maxExistTime = 3f;
        private float currentExistTime;
        private float baseMultiplier = 0.2f;
        [SerializeField] private SoundEffectController prefab;
        [SerializeField] private AudioClip impactSFX;
        public AttachmentManager manager => PlayerInputController.Instance.GetComponent<AttachmentManager>();
        public TrailRenderer a => GetComponent<TrailRenderer>();
        
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

        private void OnEnable()
        {
            a.startColor = manager.startColorOfBullets;
            a.endColor = manager.startColorOfBullets;
        }

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
            if (!fromEnemy)
            {
                if (other.gameObject.tag == "Enemy")
                {
                    var enemy = other.GetComponent<EnemyBase>();
                    damage = damage * CalculateDamageMultiplier(enemy);
                    statusEffectAttachment.ApplyEffect(enemy);
                    enemy.TakeDamage(damage);
                    DestroyBullet();
                }
            }
            else
            {
                if (other.gameObject.tag == "Player")
                {
                    var player = other.GetComponent<PlayerBase>();
                    player.TakeDamage(damage);
                    DestroyBullet();
                }
            }
            
            if (muzzleAttachment != null && muzzleAttachment.DamageType == DamageType.red)
            {
                var enemies = Physics.OverlapSphere(transform.position, 5f, LayerMask.GetMask("Enemy"));
                if (enemies.Length > 0)
                {
                    foreach (var enemy in enemies)
                    {
                        if (enemy != other)
                        {
                            var b = enemy.GetComponent<EnemyBase>();
                            if (b.DamageType != muzzleAttachment.DamageType)
                            {
                                b.TakeDamage(damage / 2);
                            }
                        }
                    }
                }

                var p = Instantiate(prefab);
                p.transform.parent = null;
                p.SetEffect(impactSFX);
                p.PlayEffect();
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
            
            return tempMultiplier;
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
        
        public void Initialize(Vector3 forward, float dmg, float speed, bool fromEnemy)
        {
            this.fromEnemy = fromEnemy;
            damage = dmg;
            bulletSpeed = speed;
            transform.forward = forward;
        }

        private void MoveBullet()
        {
            transform.position += transform.forward * bulletSpeed * Time.deltaTime;
        }

        private void DestroyBullet()
        {
            fromEnemy = false;
            currentExistTime = 0;
            BulletBasePool.Instance.ReleaseObject(this);
        }
    }
}