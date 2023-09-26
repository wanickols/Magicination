using System.Collections.Generic;

using System.IO;
using UnityEngine;

namespace MGCNTN.Core
{
    public abstract class Savable : ISavable
    {

        public Savable()
        {
            Game.manager.saveManager.addSavable(this);
        }

        ///Private Variables
        private string path => SaveManager.savePath + customPath;

        ///Protected Variables
        protected abstract string customPath { get; }
        protected abstract string errorMessage { get; }

        string ISavable.CustomPath => customPath;

        string ISavable.ErrorMessage => errorMessage;

        ///Public Functions
        public abstract bool SaveData();
        public abstract bool LoadData();

        ///Protected Functions
        protected void saveToFile(List<string> jsons) => File.WriteAllLines(path, jsons);
        protected string[] loadFromFile()
        {
            try
            {
                return File.ReadAllLines(path);
            }
            catch
            {
                Debug.LogWarning("Error: Failed to load " + path);
            }

            return null;
        }

        bool ISavable.SaveData() => this.SaveData();

        bool ISavable.LoadData() => this.LoadData();
    }
}