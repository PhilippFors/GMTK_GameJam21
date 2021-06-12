using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Entities.Player
{
    public class PlayerBase : EntityBase
    {
        public override void TakeDamage(float dmg)
        {
            throw new System.NotImplementedException();
        }

        public override void Heal(float value)
        {
            throw new System.NotImplementedException();
        }

        public override void OnDeath()
        {
            throw new System.NotImplementedException();
        }
    }
}
