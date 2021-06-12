using Attachments;
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
        
        private bool IsLMBPressed => PlayerInputController.Instance.LeftMouseButton.IsPressed;
        private float fireRate;
        private float currentTimer = 0;
        private bool canShoot;

        private void Awake()
        {
            PlayerInputController.Instance.LeftMouseButton.Started += ctx => ShootSemi();
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
                Debug.Log("Shoot auto");
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
            var bullet = BulletBasePool.Instance.GetObject();
            bullet.transform.position = bulletSpawnPoint.position;
            bullet.gameObject.SetActive(true);
            bullet.Initialize(bulletSpawnPoint.forward, baseDmg,attachmentManager.CurrentStatus, attachmentManager.CurrentMuzzle, attachmentManager.CurrentMagazine);
        }
    }
}