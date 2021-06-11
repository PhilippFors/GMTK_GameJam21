using System.Collections;
using UnityEngine;

namespace Effects
{
    [System.Serializable]
    public class SoundEffectController : IEffectController
    {
        [SerializeField] private AudioSource soundEffect;

        public override void PlayEffect()
        {
            if (PlayOnCommand)
            {
                soundEffect.Play();
            }
        }

        public override void StopEffect()
        {
            soundEffect.Stop();
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
            {
                soundEffect.Play();
            }
        }
    }
}
