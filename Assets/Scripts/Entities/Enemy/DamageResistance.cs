using UnityEngine;

namespace Entities.Enemy
{
    [System.Serializable]
    public class DamageResistance
    {
        public DamageType DamageType => damageType;
        public float Resistance => resistance;
        
        [SerializeField] DamageType damageType;
        [SerializeField, Range(0,1)] float resistance;
    }
}