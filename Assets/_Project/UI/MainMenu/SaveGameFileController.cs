using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using SLSystem;
using UnityEngine.SceneManagement;
using System;

namespace Planer
{
    public class SaveGameFileController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI SaveGameName, CreatedTimeStamp, ModifiedTimeStamp;
        private string saveGameName;
        private void Awake() => gameObject.SetActive(false);
        public void SetInfo(SaveGameFile saveGame)
        {
            this.saveGameName = saveGame.SaveGameName;
            SaveGameName.text = saveGame.SaveGameName;
            CreatedTimeStamp.text = saveGame.CreationDate.ToString();
            ModifiedTimeStamp.text = saveGame.LastModifiedDate.ToString();
            gameObject.SetActive(true);
        }

        public async void OnLoadClick()
        {
            GameDataManager.Instance.ResetGame();
            await GameDataManager.Instance.LoadGame(saveGameName);
            SceneManager.LoadScene("/Scenes/MapScene");
        }

        public void OnDeleteClick() => GameDataManager.Instance.DeleteSaveGame(saveGameName);

    }
}
