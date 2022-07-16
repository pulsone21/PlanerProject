using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.Events;

namespace UISystem
{
    public class ModalController : MonoBehaviour
    {
        [Header("Header Settings")]
        [SerializeField] private TextMeshProUGUI HeaderText;

        [Space(5f)]
        [Header("Content Settings")]
        [SerializeField] private TextMeshProUGUI ContentText;
        [SerializeField] private Image Image;

        [Space(5f)]
        [Header("Footer Settings")]
        [SerializeField] private Button confirmButton;
        [SerializeField] private TextMeshProUGUI confirmButtonText;
        [SerializeField] private Button cancleButton;
        [SerializeField] private TextMeshProUGUI cancleButtonText;
        [SerializeField] private Button alternativeButton;
        [SerializeField] private TextMeshProUGUI alternativeButtonText;
        public void ShowModal(string Header, string Content, UnityAction OnOkAction, string OkLabel = null)
        {
            ShowModal(Header, Content, OnOkAction, null, null, null, OkLabel, null, null);
        }
        public void ShowModal(string Header, string Content, UnityAction OnOkAction, UnityAction OnCancleAction = null, UnityAction OnAlternativeAction = null, Sprite image = null, string OkLabel = null, string CancleLabel = null, string alternativeLable = null)
        {
            HeaderText.text = Header;
            ContentText.text = Content;

            if (image != null)
            {
                Image.sprite = image;
                Image.gameObject.SetActive(true);
            }
            else
            {
                Image.gameObject.SetActive(false);
            }

            confirmButton.onClick.AddListener(OnOkAction);
            if (OkLabel.Length > 0) confirmButtonText.text = OkLabel;

            if (OnCancleAction != null)
            {
                cancleButton.onClick.AddListener(OnCancleAction);
                if (CancleLabel.Length > 0) cancleButtonText.text = CancleLabel;
                cancleButton.gameObject.SetActive(true);
            }
            else
            {
                cancleButton.gameObject.SetActive(false);
            }

            if (OnAlternativeAction != null)
            {
                alternativeButton.onClick.AddListener(OnAlternativeAction);
                if (alternativeLable.Length > 0) alternativeButtonText.text = alternativeLable;
                alternativeButton.gameObject.SetActive(true);
            }
            else
            {
                alternativeButton.gameObject.SetActive(false);
            }
            Show();
        }

        private void Show() => gameObject.SetActive(true);

        public void CloseModal()
        {
            PopUpWindowManager.OnClosedModal(this);
            Destroy(gameObject);
        }
    }
}
