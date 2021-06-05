using UnityEngine;
using System.Collections;

namespace General.Effects
{
    public abstract class IEffectController : MonoBehaviour
    {
        public string EffectName => effectName;
        public bool PlayOnCollision => playOnCollision;
        public bool PlayOnCommand => playOnCommand;
        public bool Loop => loop;
        public bool Active => active;

        [SerializeField] protected string effectName;
        [SerializeField] protected bool playOnCollision;
        [SerializeField] protected bool playOnCommand;
        [SerializeField] protected bool loop;
        [SerializeField] protected bool active;
        protected Coroutine LoopCoroutine;

        protected abstract IEnumerator LoopEffect();
        public abstract void StopLoop();
        public abstract void PlayEffect();
        public abstract void StopEffect();
    }
}
