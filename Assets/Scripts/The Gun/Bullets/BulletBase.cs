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
        [SerializeField] private SoundEffectController prefab;
        [SerializeField] private AudioClip impactSFX;
        
        private bool fromEnemy;
        private float damage;
        private float maxExistTime = 3f;
        private float currentExistTime;
        private float baseMultiplier = 0.2f;

        private AttachmentManager manager;
        private TrailRenderer trailRenderer;
       
        private Gradient grad;
        private GradientColorKey[] colorKey;
        private GradientAlphaKey[] alphaKey;
        
        public GameObject redParticleEffect;
        public GameObject blueParticleEffect;
        public GameObject greenParticleEffect;
        private void Start()
        {
           manager = PlayerInputController.Instance.GetComponent<AttachmentManager>();
           trailRenderer = GetComponent<TrailRenderer>();
        }

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
            trailRenderer.enabled = true;
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
                    //Instantiate(particleEffectPrefab, transform).GetComponent<ParticleSystem>().Play();
                    switch (muzzleAttachment.DamageType)
                    {
                        case DamageType.blue:
                            Instantiate(blueParticleEffect, transform.position, Quaternion.identity);
                            break;
                        case DamageType.red:
                            Instantiate(redParticleEffect, transform.position, Quaternion.identity);
                            break;
                        case DamageType.green:
                            Instantiate(greenParticleEffect, transform.position, Quaternion.identity);
                            break;
                    }
                   
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
                    //Instantiate(particleEffectPrefab, transform).GetComponent<ParticleSystem>().Play();
                    Instantiate(redParticleEffect, transform.position, Quaternion.identity);
                    player.TakeDamage(damage);
                    DestroyBullet();
                }
            }
            
            if (!fromEnemy && muzzleAttachment != null && muzzleAttachment.DamageType == DamageType.red)
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
           
            /*var b = particle.colorOverLifetime;

            grad = new Gradient();
            
            colorKey = new GradientColorKey[2];
            colorKey[0].color = manager.startColorOfBullets;
            colorKey[0].time = 0.0f;
            colorKey[1].color = manager.startColorOfBullets;
            colorKey[1].time = 1.0f;
          
            alphaKey = new GradientAlphaKey[2];
            alphaKey[0].alpha = 1.0f;
            alphaKey[0].time = 0.0f;
            alphaKey[1].alpha = 0.0f;
            alphaKey[1].time = 1.0f;
            grad.SetKeys(colorKey, alphaKey);

            b.color = grad;*/
            
            trailRenderer.Clear();
            trailRenderer.time = 0.1f;
            trailRenderer.startColor = manager.startColorOfBullets;
            trailRenderer.endColor = manager.startColorOfBullets;
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
            trailRenderer.time = 0;
            trailRenderer.enabled = false;
            BulletBasePool.Instance.ReleaseObject(this);
        }
    }
}
