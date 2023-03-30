using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace SLSystem
{
    public abstract class DataHandler : ScriptableObject
    {
        [SerializeField] protected bool _useEcnryption = false;
        protected readonly string _encryptionKeyWord = "DeineMuddaStinktNachFisch";
        public abstract void Initilize();
        public abstract void Serialize(GameData gameData);
        public abstract Task<GameData> Deserialize(string filename);
        public abstract Task<List<SaveGameFile>> LoadAllSaveGames();
        public abstract void DeleteSaveGame(string filename);

        // Simple XOR Encryption Algorythm
        protected string EncryptDecrypt(string data)
        {
            string modifiedData = "";
            for (int i = 0; i < data.Length; i++) modifiedData += (char)(data[i] ^ _encryptionKeyWord[i % _encryptionKeyWord.Length]);
            return modifiedData;
        }
    }
}
