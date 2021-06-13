using System.Collections;
using Entities.Enemy;
using Entities.Enemy.AI.ProjectileMan;
using UnityEngine;

namespace Attachments.StatusAttachments
{
    [CreateAssetMenu]
    public class SlowStatus : StatusEffectAttachment
    {
        public override void ApplyEffect(EnemyBase enemy)
        {
            enemy.GetComponent<EnemyAttack>().SlowEffect(CalcStrength(enemy, strength), effectCooldown);
        }
    }
}