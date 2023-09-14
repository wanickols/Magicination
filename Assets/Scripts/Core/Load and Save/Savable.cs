using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace MGCNTN.Core
{
    public abstract class Savable : MonoBehaviour
    {

        ///private parameters
        private string path => SaveManager.savePath + customPath;

        ///Protected Parameter
        protected abstract string customPath { get; }

        ///Unity Functions
        protected virtual void Awake() => Game.manager.saveManager.addSavable(this);


        ///Public Functions
        public virtual string errorMessage { get; }
        public abstract bool SaveData();
        public abstract bool LoadData();

        ///Protected Functions
        protected void saveToFile(List<string> jsons) => File.WriteAllLines(path, jsons);
        protected string[] loadFromFile() => File.ReadAllLines(path);

    }
}