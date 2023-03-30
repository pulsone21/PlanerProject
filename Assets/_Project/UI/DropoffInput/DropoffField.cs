using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using VehicleSystem;
using UnityEngine.Events;
using DG.Tweening;

namespace UISystem
{
    public abstract class DropoffField<T> : MonoBehaviour
    {
        [SerializeField] protected TextMeshProUGUI DisplayText;
        [SerializeField] protected string DefaultText;
        protected T currentValue;
        private Tween _myTween;
        private void Start() => DisplayText.text = DefaultText;
        public void DropOff(T value, string displayText)
        {
            currentValue = value;
            DisplayText.text = displayText;
            SaveValue();
        }
        protected abstract void SaveValue();
        public abstract void ClearValue();
        public virtual void StartHighlight()
        {
            Debug.Log("Highlighting!!!");
            _myTween = DisplayText.DOColor(Color.white, 0.5f).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
        }
        public virtual void StopHighlight()
        {
            Debug.Log("Stop Highlighting");
            _myTween.Rewind();
            _myTween.Kill();
        }

    }
}
