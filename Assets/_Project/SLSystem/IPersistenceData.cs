using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SLSystem
{
    public interface IPersistenceData
    {
        GameObject This { get; }
        void Load(GameData gameData);
        void Save(ref GameData gameData);
    }
}
