
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

namespace UISystem
{
    [RequireComponent(typeof(CanvasGroup))]
    public class ReordableListItem : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        private Transform _parentList;
        private int _originalIndex, _newIndex, _siblingCount;
        public CanvasGroup CanvasGroup;
        private Vector3 _originalPos, _currentPos;
        private ReordableListItem _visualCopy;
        private Scrollbar _scrollbar;
        private float _topHeight, _bottomHeight;
        private RectTransform _myRect;
        private bool _isDragging;
        private bool _initialized = false;
        public Action OnPositionChange;
        public void Initialize(bool isCopy = false)
        {
            if (_initialized) return;
            CanvasGroup = GetComponent<CanvasGroup>();
            _initialized = true;
            if (isCopy) return;
            _isDragging = false;
            _parentList = transform.parent;
            ScrollRect scrollRect = GetComponentInParent<ScrollRect>();
            _topHeight = scrollRect.transform.position.y;
            _bottomHeight = _topHeight - scrollRect.GetComponent<RectTransform>().sizeDelta.y;
            _scrollbar = scrollRect.verticalScrollbar;
            _myRect = GetComponent<RectTransform>();
        }
        private void Update()
        {
            if (_isDragging)
            {
                if (_currentPos.y > _topHeight - _myRect.sizeDelta.y * 0.8f) _scrollbar.value = Mathf.Lerp(_scrollbar.value, 1f, Time.fixedDeltaTime * 1.5f);
                if (_currentPos.y < _bottomHeight + _myRect.sizeDelta.y * 0.8f) _scrollbar.value = Mathf.Lerp(_scrollbar.value, 0f, Time.fixedDeltaTime * 1.5f);
            }
        }
        public void OnBeginDrag(PointerEventData eventData)
        {
            _isDragging = true;
            _originalIndex = transform.GetSiblingIndex();
            _siblingCount = _parentList.childCount;
            _newIndex = _originalIndex;
            _originalPos = transform.position;
            foreach (CanvasGroup cG in GetComponentsInChildren<CanvasGroup>()) cG.ignoreParentGroups = false;

            _visualCopy = Instantiate(this);
            _visualCopy.Initialize(true);
            _visualCopy.transform.SetParent(transform.root, true);
            _visualCopy.GetComponent<RectTransform>().sizeDelta = GetComponent<RectTransform>().sizeDelta;

            CanvasGroup.DOFade(0f, 0.3f);
            _visualCopy.CanvasGroup.DOFade(0.8f, 0.1f);
            _visualCopy.CanvasGroup.blocksRaycasts = false;
            _visualCopy.CanvasGroup.interactable = false;
        }

        public void OnDrag(PointerEventData eventData)
        {
            Vector3 newPos = new Vector3(eventData.position.x, eventData.position.y, _originalPos.z);
            _visualCopy.transform.position = newPos;
            _currentPos = newPos;

            int aboveIndex = _newIndex - 1;
            if (aboveIndex >= 0)
            {
                Vector3 aboveSiblingPos = _parentList.GetChild(aboveIndex).position;
                if (newPos.y > aboveSiblingPos.y)
                {
                    _newIndex = aboveIndex;
                    transform.SetSiblingIndex(_newIndex);
                    return;
                }
            }

            int belowIndex = _newIndex + 1;
            if (belowIndex < _siblingCount)
            {
                Vector3 belowSiblinPos = _parentList.GetChild(belowIndex).position;
                if (newPos.y < belowSiblinPos.y)
                {
                    _newIndex = belowIndex;
                    transform.SetSiblingIndex(_newIndex);
                    return;
                }
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _isDragging = false;
            transform.SetParent(_parentList);
            transform.SetSiblingIndex(_newIndex);
            if (_newIndex != _originalIndex) OnPositionChange?.Invoke();
            CanvasGroup.DOFade(1f, 0.3f);
            foreach (CanvasGroup cG in GetComponentsInChildren<CanvasGroup>()) cG.ignoreParentGroups = true;
            _visualCopy.CanvasGroup.DOFade(0f, 0.1f).onComplete += () => Destroy(_visualCopy.gameObject);
        }
    }
}
