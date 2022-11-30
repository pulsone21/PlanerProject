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
        public void OnNewGameClick()
        {
            DisableAllButtons();
            NewGamePanel.SetActive(true);
            SettingsPanel.SetActive(false);
            EnableAllButtons();
        }

        public async void OnLoadGameClick()
        {
            DisableAllButtons();
            List<SaveGameFile> files = await GameDataManager.Instance.LoadAllSaveGames();
            listController.LoadSaveGames(files);
            EnableAllButtons();
        }

        public void OnContinueClick()
        {
            DisableAllButtons();

        }

        public void OnSettingsClick()
        {
            DisableAllButtons();
            SettingsPanel.SetActive(true);
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
