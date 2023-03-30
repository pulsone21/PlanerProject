using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SLSystem
{
    [CreateAssetMenu(fileName = "FileDataHandler", menuName = "SLSystem/FileDataHandler")]
    public class FileDataHandler : DataHandler
    {
        [SerializeField, Tooltip("With an '.' in the begining.")] private string _fileExtension;
        [SerializeField] private bool _useSystemPath;
        [SerializeField] private string _pathToFile;
        public override async Task<GameData> Deserialize(string fileName)
        {
            if (fileName.Split(".").Length > 1) fileName = fileName.Split(".")[0];
            GameData loadedData = null;
            string file = fileName + _fileExtension;
            string fullPath = CalculatePath(file);
            if (File.Exists(fullPath))
            {
                try
                {
                    string dataToLoad = "";
                    using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                    {
                        using (StreamReader reader = new StreamReader(stream))
                        {
                            dataToLoad = reader.ReadToEnd();
                        }
                    }
                    if (_useEcnryption) dataToLoad = EncryptDecrypt(dataToLoad);
                    loadedData = JsonUtility.FromJson<GameData>(dataToLoad);
                    loadedData.SetDataDict();

                }
                catch (Exception e)
                {
                    Debug.LogError("Error occured during loading game data from file: " + fullPath + "\n" + e);
                }
            }
            return loadedData;
        }

        public override void Serialize(GameData gameData)
        {
            string fileName = gameData.SaveName + _fileExtension;
            string fullPath = CalculatePath(fileName);
            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
                gameData.GenerateSaveableList();
                string dataToStore = JsonUtility.ToJson(gameData, true);
                if (_useEcnryption) dataToStore = EncryptDecrypt(dataToStore);
                using (FileStream stream = new FileStream(fullPath, FileMode.Create))
                {
                    using (StreamWriter writer = new StreamWriter(stream))
                    {
                        writer.Write(dataToStore);
                    }
                }
            }
            catch (Exception e)
            {
                Debug.LogError("Error occured during saving game data to file: " + fullPath + "\n" + e);
            }
        }

        public override async Task<List<SaveGameFile>> LoadAllSaveGames()
        {
            List<SaveGameFile> gameDatas = new List<SaveGameFile>();
            string[] Files = Directory.GetFiles(PathToFile);
            foreach (string file in Files)
            {
                FileInfo saveGameFile = new FileInfo(file);
                string FileName = file.Split("/").Last().Split(".").First();
                DateTime creationDate = saveGameFile.CreationTime;
                DateTime modifiedDate = saveGameFile.LastWriteTime;
                if (file.Contains(_fileExtension)) gameDatas.Add(new(FileName, creationDate, modifiedDate));
            }
            return gameDatas;
        }

        private string CalculatePath(string file) => Path.Combine(PathToFile, file);

        public string PathToFile
        {
            get
            {
                if (_useSystemPath) return Application.persistentDataPath;
                return _pathToFile;
            }
        }
        public override void Initilize() => Debug.Log("FileDataHandler Nothing to Initilize");

        public override void DeleteSaveGame(string filename)
        {
            string file = filename + _fileExtension;
            string fullPath = CalculatePath(file);
            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }
        }
    }


}
