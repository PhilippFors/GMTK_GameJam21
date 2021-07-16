using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Entities.Player
{
    public class PlayerBase : EntityBase
    {
        public CameraShakeValues cameraShakeValues;
        public override void TakeDamage(float dmg)
        {
            currentHealth -= dmg;
            CameraShake.Instance.ActivateShake(cameraShakeValues);
            if(currentHealth <= 0)
                OnDeath();
        }

        public override void Heal(float value)
        {
            
        }

        [Button()]
        public override void OnDeath()
        {
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
