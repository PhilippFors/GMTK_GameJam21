using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace General.Effects
{
    /// <summary>
    /// Used as a simple extension for Particle Systems for ease of use. Can be referenced in the EffectController.
    /// </summary>

    [System.Serializable]
    public class ParticleEffectController : IEffectController
    {
        [SerializeField] private ParticleSystem particle;

        public override void StopEffect()
        {
            particle.Stop();
        }

        public override void PlayEffect()
        {
            if (PlayOnCommand)
            {
                particle.Play();
            }
        }

        public override void StopLoop()
        {
            throw new System.NotImplementedException();
        }
        protected override IEnumerator LoopEffect()
        {
            throw new System.NotImplementedException();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (PlayOnCollision && Active)
                PlayEffect();
        }
    }
}
