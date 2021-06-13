using Entities.Enemy;
using UnityEngine;
namespace Attachments.StatusAttachments
{
    [CreateAssetMenu]
    public class DoTStatus : StatusEffectAttachment
    {
        public override void ApplyEffect(EnemyBase enemy)
        {
            enemy.GetComponent<EnemyAttack>().DamageOverTime(CalcStrength(enemy, strength), effectCooldown);
        }
    }
}