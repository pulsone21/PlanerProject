using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
using System;

namespace SLSystem
{
    public class GameDataManager : MonoBehaviour
    {
        private enum SLStyle { File, Cloud };
        public static GameDataManager Instance;
        [SerializeField] private DataHandler _dataHandler;
        private GameData _gameData;
        private List<IPersistenceData> _persistenceData;
        [SerializeField] private List<GameObject> persDataObj;

        [Header("Debuging Stuff")]
        [SerializeField] private bool _initializeDataIfNull;

        [Header("Saving Settings")]
        [SerializeField] private bool _autoSaving = true;
        [SerializeField, Tooltip("AutoSaving Intervall in minutes, Default is 5")] private float _autoSavingIntervall = 5f;
        private float _autoSavingIntervallInSeconds;
        private float currentSeconds = 0f;

        public string SaveGameName;
        private void Awake()
        {
            if (Instance)
            {
                DestroyImmediate(this.gameObject);
                return;
            }
            else
            {
                Instance = this;
            }
            DontDestroyOnLoad(this.gameObject);
            _autoSavingIntervallInSeconds = _autoSavingIntervall * 60f;
        }

        private void Start() => _dataHandler.Initilize();

        private void Update()
        {
            if (_gameData != null && _autoSaving) AutoSave();
        }

        private void AutoSave()
        {
            currentSeconds += Time.deltaTime;
            if (currentSeconds >= _autoSavingIntervallInSeconds)
            {
                SaveGame();
                Debug.Log("AutoSaving Game");
                currentSeconds = 0f;
            }
        }

        public void NewGame(string saveGameName)
        {
            SaveGameName = saveGameName;
            _gameData = new GameData(saveGameName);
        }

        public async Task LoadGame(string fileName)
        {
            Debug.Log("Loading Game");
            _gameData = await _dataHandler.Deserialize(fileName);
            if (this._gameData == null && _initializeDataIfNull)
            {
                if (SaveGameName == null)
                {
                    Debug.LogError("No SaveGameName set please Set befor creating new saveGame");
                    return;
                }
                NewGame(SaveGameName);
            }
            if (this._gameData == null)
            {
                Debug.Log("No GameData found, start a new Game first!");
                return;
            }

            _gameData.SetDataDict();
        }

        private void PushGameDataToObjects()
        {
            foreach (IPersistenceData obj in _persistenceData) obj.Load(_gameData);
        }

        public void SaveGame()
        {
            if (_gameData == null)
            {
                Debug.LogError("No Game Started, cannot save nothing!");
                return;
            }
            foreach (IPersistenceData obj in _persistenceData) obj.Save(ref _gameData);
            Debug.Log("Saved all GameData");
            Debug.Log(_gameData.Data.Count);
            _dataHandler.Serialize(_gameData);
        }
        public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            _persistenceData = FindPersistenceDataObjects();
            Debug.Log("Found All _persistenceDataObjects");
            if (scene.name != "MainMenu") PushGameDataToObjects();
        }
        public void OnSceneUnloaded(Scene scene) => SaveGame();
        public bool HasSaveData() => _gameData != null;

        private void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
            SceneManager.sceneUnloaded += OnSceneUnloaded;
        }
        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            SceneManager.sceneUnloaded -= OnSceneUnloaded;
        }
        private void OnApplicationQuit() => SaveGame();

        public void DeleteSaveGame(string filename) => _dataHandler.DeleteSaveGame(filename);

        public async void DeleteAllSaveGames()
        {
            List<SaveGameFile> saveGames = await _dataHandler.LoadAllSaveGames();
            foreach (SaveGameFile saveGame in saveGames)
            {
                Debug.Log("Deleting SaveGame: " + saveGame.SaveGameName);
                DeleteSaveGame(saveGame.SaveGameName);
            }
        }
        private List<IPersistenceData> FindPersistenceDataObjects()
        {
            persDataObj = new List<GameObject>();
            IEnumerable<IPersistenceData> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>().OfType<IPersistenceData>();
            foreach (IPersistenceData item in dataPersistenceObjects)
            {
                persDataObj.Add(item.This);
            }
            return new List<IPersistenceData>(dataPersistenceObjects);
        }
        public async Task<List<SaveGameFile>> LoadAllSaveGames() => await _dataHandler.LoadAllSaveGames();

        public void ResetGame()
        {
            _gameData = null;
        }
    }
}
