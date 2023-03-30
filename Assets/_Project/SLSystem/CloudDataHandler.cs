using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Authentication;
using Unity.Services.CloudSave;
using System.Threading.Tasks;
using Unity.Services.Core.Environments;

namespace SLSystem
{
    [CreateAssetMenu(fileName = "CloudDataHandler", menuName = "SLSystem/CloudDataHandler")]
    public class CloudDataHandler : DataHandler
    {
        public enum Environment { Production, Development }
        [SerializeField] private Environment env;
        private async void InitializeAsync()
        {
            var options = new InitializationOptions();
            options.SetEnvironmentName(env.ToString().ToLower());
            await UnityServices.InitializeAsync(options);
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
        }

        public override async Task<GameData> Deserialize(string filename)
        {
            try
            {
                Dictionary<string, string> cloudData = await CloudSaveService.Instance.Data.LoadAsync(new HashSet<string> { filename });
                string dataToLoad = cloudData[filename];
                if (_useEcnryption) dataToLoad = EncryptDecrypt(dataToLoad);
                GameData loadedData = new GameData(filename);
                loadedData = JsonUtility.FromJson<GameData>(dataToLoad);
                loadedData.SetDataDict();
                return loadedData;
            }
            catch (CloudSaveValidationException e) { throw e; }
            catch (CloudSaveRateLimitedException e) { throw e; }
            catch (CloudSaveException e) { throw e; }
        }

        public override async Task<List<SaveGameFile>> LoadAllSaveGames()
        {
            try
            {
                List<SaveGameFile> gameFiles = new List<SaveGameFile>();
                List<string> cloudData = await CloudSaveService.Instance.Data.RetrieveAllKeysAsync();
                foreach (string entry in cloudData)
                {
                    gameFiles.Add(new(entry));
                }
                return gameFiles;
            }
            catch (CloudSaveValidationException e) { throw e; }
            catch (CloudSaveRateLimitedException e) { throw e; }
            catch (CloudSaveException e) { throw e; }
        }
        public override void Serialize(GameData gameData)
        {
            gameData.GenerateSaveableList();
            string dataToStore = JsonUtility.ToJson(gameData, true);
            if (_useEcnryption) dataToStore = EncryptDecrypt(dataToStore);
            SaveToCloud(new Dictionary<string, string> { { gameData.SaveName, dataToStore } });
        }
        private async void SaveToCloud(Dictionary<string, string> gameData)
        {
            await CloudSaveService.Instance.Data.ForceSaveAsync(ConvertFromGameData(gameData));
        }
        private Dictionary<string, object> ConvertFromGameData(Dictionary<string, string> gameData)
        {
            Dictionary<string, object> outDict = new();
            foreach (KeyValuePair<string, string> entry in gameData)
            {
                outDict[entry.Key] = entry.Value;
            }
            return outDict;
        }
        public override void Initilize() => InitializeAsync();
        public override async void DeleteSaveGame(string filename)
        {
            await CloudSaveService.Instance.Data.ForceDeleteAsync(filename);
        }
    }
}
