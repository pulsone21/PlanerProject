using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SLSystem;
using TMPro;

namespace Planer
{
    public class SaveGameListController : MonoBehaviour
    {
        [SerializeField] private Transform ContentContainer;
        [SerializeField] private SaveGameFileController ContentPrefab;
        [SerializeField] private GameObject DefaultPrefab;

        private void OnDisable() => ClearCurrentTable();
        public void LoadSaveGames(List<SaveGameFile> saveGames)
        {
            ClearCurrentTable();
            if (saveGames.Count > 0)
            {
                foreach (SaveGameFile saveGame in saveGames)
                {
                    SaveGameFileController newPrefab = Instantiate(ContentPrefab, Vector3.zero, Quaternion.identity);
                    newPrefab.SetInfo(saveGame);
                    newPrefab.transform.SetParent(ContentContainer);
                }
            }
            else
            {
                GameObject prefab = Instantiate(DefaultPrefab, Vector3.zero, Quaternion.identity);
                prefab.transform.SetParent(ContentContainer);
                prefab.transform.localScale = Vector3.one;
                prefab.transform.localPosition = new Vector3(0, 0, 0);
            }

            gameObject.SetActive(true);
        }

        private void ClearCurrentTable()
        {
            while (ContentContainer.childCount > 0)
            {
                Destroy(ContentContainer.GetChild(0));
            }
        }
    }
}
