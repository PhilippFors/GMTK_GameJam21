using Entities.Enemy;
using UnityEngine;

namespace Attachments.StatusAttachments
{
    [CreateAssetMenu]
    public class EnemyExplodingStatus : StatusEffectAttachment
    {
        public override void ApplyEffect(EnemyBase enemy)
        {
            enemy.GetComponent<EnemyAttack>().ExplodingEnemy(CalcStrength(enemy, strength), effectCooldown);
        }
    }
}
