using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace UISystem
{
    public class ContentPanelManager : MonoBehaviour
    {
        private GameObject currentPanel;
        public static ContentPanelManager Instance;
        [SerializeField] private Button mainPanelBtn;
        [SerializeField] private TextMeshProUGUI PanelName;

        private Stack<GameObject> prevPanels = new Stack<GameObject>();
        private Stack<GameObject> nextPanels = new Stack<GameObject>();


        private void Awake()
        {
            if (Instance)
            {
                DestroyImmediate(this);
            }
            else
            {
                Instance = this;
            }
        }

        private void Start()
        {
            InitializePages();
        }

        private void InitializePages()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }

            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
            mainPanelBtn.onClick?.Invoke();
        }

        public static void SetPanelStatic(GameObject Panel) => Instance.SetPanel(Panel);

        public void SetPanel(GameObject Panel)
        {
            if (currentPanel != null)
            {
                currentPanel.SetActive(false);
                prevPanels.Push(currentPanel);
            }
            currentPanel = Panel;
            currentPanel.SetActive(true);
        }

        public void ToPreviousPanel()
        {
            if (currentPanel) currentPanel.SetActive(false);
            if (prevPanels.Count < 1)
            {
                mainPanelBtn.onClick.Invoke();
                return;
            }
            else
            {
                nextPanels.Push(prevPanels.Peek());
                currentPanel = prevPanels.Pop();
                currentPanel.SetActive(true);
            }
        }

        public void SetPanelName(string panelName) => PanelName.text = panelName;

        public void ToNextPanel()
        {
            if (nextPanels.Count < 1) return;
            if (currentPanel) currentPanel.SetActive(false);
            prevPanels.Push(nextPanels.Peek());
            currentPanel = nextPanels.Pop();
            currentPanel.SetActive(true);
        }

        private void OnDisable()
        {
            prevPanels.Clear();
        }
    }
}

