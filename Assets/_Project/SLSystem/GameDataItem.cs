using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SLSystem
{
    [Serializable]
    public class GameDataItem
    {
        public string ClassName;
        public string Object;

        public GameDataItem(string obj, string className)
        {
            Debug.Log("Creating new GameDataItem, ClassName:" + className + " object: " + obj);
            ClassName = className;
            Object = obj;
        }
    }
}
