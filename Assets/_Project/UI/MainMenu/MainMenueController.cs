using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SLSystem;
using UnityEngine.UI;

namespace Planer
{
    public class MainMenueController : MonoBehaviour
    {
        [SerializeField] private List<Button> Buttons;
        [SerializeField] private SaveGameListController listController;
        [SerializeField] private GameObject SettingsPanel, NewGamePanel;
        private GameObject ActivePanel;
        public void OnNewGameClick()
        {
            DisableAllButtons();
            if (ActivePanel != null) ActivePanel.SetActive(false);
            NewGamePanel.SetActive(true);
            ActivePanel = NewGamePanel;
            EnableAllButtons();
        }

        public async void OnLoadGameClick()
        {
            DisableAllButtons();
            if (ActivePanel != null) ActivePanel.SetActive(false);
            List<SaveGameFile> files = await GameDataManager.Instance.LoadAllSaveGames();
            listController.LoadSaveGames(files);
            ActivePanel = listController.gameObject;
            EnableAllButtons();
        }

        public void OnContinueClick()
        {
            DisableAllButtons();

        }

        public void OnSettingsClick()
        {
            DisableAllButtons();
            if (ActivePanel != null) ActivePanel.SetActive(false);
            SettingsPanel.SetActive(true);
            ActivePanel = SettingsPanel;
            EnableAllButtons();
        }
        public void OnExitClick()
        {
            DisableAllButtons();
            Application.Quit();
        }

        private void DisableAllButtons()
        {
            foreach (Button button in Buttons)
            {
                button.interactable = false;
            }
        }

        public void EnableAllButtons()
        {
            foreach (Button button in Buttons)
            {
                button.interactable = true;
            }
        }
    }
}
