using System;
using UnityEngine;

namespace Assets.Codebase
{
    public class AnimationStatusProvider: MonoBehaviour
    {
        [SerializeField] private AnimationClip _animation;

        public event Action AnimationCompleted;

        public void OnAnimationCompleted() 
        { 
            AnimationCompleted?.Invoke();
        }
    }
}
