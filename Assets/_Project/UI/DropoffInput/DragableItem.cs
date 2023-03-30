using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VehicleSystem;
using DG.Tweening;
using UnityEngine.EventSystems;
using System;

namespace UISystem
{
    [RequireComponent(typeof(CanvasGroup))]
    public abstract class DragableItem : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] protected CanvasGroup CanvasGroup;
        protected Transform orignalParent;
        protected virtual void Awake() => CanvasGroup = GetComponent<CanvasGroup>();
        protected virtual void Start() => orignalParent = transform.parent;
        public virtual void OnBeginDrag(PointerEventData eventData)
        {
            CanvasGroup.alpha = .5f;
            CanvasGroup.blocksRaycasts = false;
            transform.SetParent(transform.root);
        }

        public void OnDrag(PointerEventData eventData)
        {
            transform.position = eventData.position;
        }

        public virtual void OnEndDrag(PointerEventData eventData)
        {
            CanvasGroup.alpha = 1f;
            CanvasGroup.blocksRaycasts = true;
            if (!ValidDropOff(eventData)) transform.SetParent(orignalParent);

        }
        protected abstract bool ValidDropOff(PointerEventData eventData);
        public void OnPointerEnter(PointerEventData eventData) => CanvasGroup.DOFade(0.8f, 0.2f);
        public void OnPointerExit(PointerEventData eventData) => CanvasGroup.DOFade(1f, 0.2f);

    }
}
