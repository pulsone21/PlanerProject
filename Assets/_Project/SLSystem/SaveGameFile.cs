using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SLSystem
{
    public class SaveGameFile
    {
        public readonly string SaveGameName;
        public readonly DateTime CreationDate;
        public readonly DateTime LastModifiedDate;

        public SaveGameFile(string saveGameName, DateTime creationDate = default, DateTime lastModifiedDate = default)
        {
            SaveGameName = saveGameName;
            CreationDate = creationDate;
            LastModifiedDate = lastModifiedDate;
        }
    }
}
