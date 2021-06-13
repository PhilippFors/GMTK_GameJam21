using Attachments;
using Effects;
using Entities.Player.PlayerInput;
using ObjectPool;
using UnityEngine;

namespace TheGun
{
    public class Gun : MonoBehaviour
    {
        [SerializeField] private AttachmentManager attachmentManager;
        [SerializeField] private float baseDmg = 5f;
        [SerializeField] private bool isSemiAuto;
        [SerializeField] private Transform bulletSpawnPoint;

        [SerializeField] private Animator animator;
        private bool IsLMBPressed => PlayerInputController.Instance.LeftMouseButton.IsPressed;
        private float fireRate;
        private float currentTimer = 0;
        private bool canShoot;

        [SerializeField] private SoundEffectController sfx;
        private void Awake()
        {
            PlayerInputController.Instance.LeftMouseButton.Started += ctx => ShootSemi();
            PlayerInputController.Instance.LeftMouseButton.Canceled += ctx => animator.SetLayerWeight(1, 0);
        }

        private void Update()
        {
            fireRate = 1f / attachmentManager.CurrentMagazine.FireRate;
            ShootAuto();
            ShootTimer();
        }

        private void ShootSemi()
        {
            if (isSemiAuto && canShoot)
            {
                currentTimer -= fireRate;
                ShootBullet();
            }
        }

        private void ShootAuto()
        {
            if (!isSemiAuto && IsLMBPressed && canShoot)
            {
                currentTimer -= fireRate;
                ShootBullet();
            }
        }

        private void ShootTimer()
        {
            if (currentTimer >= fireRate)
            {
                if (!canShoot)
                {
                    canShoot = true;
                }
            }
            else
            {
                currentTimer += Time.deltaTime;
                canShoot = false;
            }
        }

        public void SetShootingType(bool value)
        {
            isSemiAuto = value;
        }

        private void ShootBullet()
        {
            animator.SetLayerWeight(1, 1);
            animator.Play("Shoot");
            
            sfx.PlayEffect();
            var bullet = BulletBasePool.Instance.GetObject();
            bullet.transform.position = bulletSpawnPoint.position;
            bullet.gameObject.SetActive(true);
            bullet.Initialize(bulletSpawnPoint.forward, baseDmg,attachmentManager.CurrentStatus, attachmentManager.CurrentMuzzle, attachmentManager.CurrentMagazine);
        }
    }
}