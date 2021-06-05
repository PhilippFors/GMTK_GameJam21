using System;
using NaughtyAttributes;
using UnityEngine;

namespace DataContainer
{
    [Serializable]
    public class FloatReference
    {
        [SerializeField] private bool useConstant = false;
        [SerializeField, EnableIf("useConstant")] private float ConstantValue;
        [SerializeField] private FloatVariable variable;

        public float Value
        {
            get
            {
                return useConstant ? ConstantValue : variable.value;
            }
            set
            {
                variable.value = value;
            }
        }
    }
}
