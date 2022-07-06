using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UISystem
{
    public abstract class SimpleFader : MonoBehaviour
    {
        [Header("Fade Adjustments")]
        [SerializeField, Range(0f, 1f)] protected float FadeDuration;
        [SerializeField, Range(0f, 1f)] protected float FadeDelay;

        public abstract void FadeIn();
        public abstract void FadeOut();
    }
}
