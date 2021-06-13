using System;
using System.Collections;
using UnityEngine;

namespace Effects
{
    [System.Serializable]
    public class SoundEffectController : IEffectController
    {
        
        [SerializeField] private AudioSource soundEffect;
        [SerializeField] private bool playOnStartup;
        [SerializeField] private bool destroyOnFinish;
        private void Start()
        {
            soundEffect.loop = loop;
            if (playOnStartup)
            {
                PlayEffect();
            }
        }

        public void SetEffect(AudioClip clip)
        {
            soundEffect.clip = clip;
        }
        public override void PlayEffect()
        {
            if (PlayOnCommand)
            {
                if (soundEffect.isPlaying)
                {
                    StopEffect();
                }
                soundEffect.Play();
            }

            if (destroyOnFinish)
            {
                StartCoroutine(DestroyOnFinish());
            }
        }

        public override void StopEffect()
        {
            soundEffect.Stop();
        }

        public override void StopLoop()
        {
            
        }

        protected override IEnumerator LoopEffect()
        {
            yield break;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (PlayOnCollision && Active)
            {
                soundEffect.Play();
            }
        }

        private IEnumerator DestroyOnFinish()
        {
            yield return new WaitForSeconds(2f);
            Destroy(gameObject);
        }
    }
}
