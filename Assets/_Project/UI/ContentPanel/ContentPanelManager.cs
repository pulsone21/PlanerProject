using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UISystem
{
    public class ContentPanelManager : MonoBehaviour
    {
        private GameObject currentPanel;
        public static ContentPanelManager Instance;
        [SerializeField] private GameObject mainPanel;

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
            if (currentPanel == null) SetPanel(mainPanel);
        }

        public void SetPanel(GameObject Panel)
        {
            if (currentPanel)
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
                currentPanel = mainPanel;
                currentPanel.SetActive(true);
                return;
            }
            else
            {
                nextPanels.Push(prevPanels.Peek());
                currentPanel = prevPanels.Pop();
                currentPanel.SetActive(true);
            }
        }

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

