using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Entities.Player
{
    public class PlayerBase : EntityBase
    {
        public override void TakeDamage(float dmg)
        {
            currentHealth -= dmg;
            Debug.Log($"Player takes {dmg} damage");
        }

        public override void Heal(float value)
        {
            
        }

        public override void OnDeath()
        {
            
        }
    }
}
