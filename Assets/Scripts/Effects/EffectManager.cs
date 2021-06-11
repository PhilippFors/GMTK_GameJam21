using System.Collections.Generic;
using UnityEngine;

namespace Effects
{
    /// <summary>
    /// Keeps reference to all Effects (SFX, Particle Systems) on an object and is used for playing effects 
    /// </summary>
    public class EffectManager : MonoBehaviour
    {
        [SerializeField] private List<SoundEffectController> soundFX = new List<SoundEffectController>();
        [SerializeField] private List<ParticleEffectController> particleFX = new List<ParticleEffectController>();

        public void PlaySoundEffect(string name)
        {
            if (soundFX.Exists(x => x.EffectName.Equals(name)))
            {
                soundFX.Find(x => x.EffectName.Equals(name)).PlayEffect();
            }
        }

        public void PlayParticleEffect(string name)
        {
            if (particleFX.Exists(x => x.EffectName.Equals(name)))
            {
                particleFX.Find(x => x.EffectName.Equals(name)).PlayEffect();
            }
        }

        public void StopSoundEffect(string name)
        {
            if (soundFX.Exists(x => x.EffectName.Equals(name)))
            {
                soundFX.Find(x => x.EffectName.Equals(name)).StopEffect();
            }
        }

        public void StopParticleEffect(string name)
        {
            if (particleFX.Exists(x => x.EffectName.Equals(name)))
            {
                particleFX.Find(x => x.EffectName.Equals(name)).StopEffect();
            }
        }
    }
}
