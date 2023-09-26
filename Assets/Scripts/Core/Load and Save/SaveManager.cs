using System.Collections.Generic;
using UnityEngine;

namespace MGCNTN.Core
{
    public class SaveManager
    {
        public static string savePath = "Assets/Saves/";

        private List<ISavable> saveObjects = new List<ISavable>();

        public bool Load()
        {
            bool success = true;
            for (int i = 0; i < saveObjects.Count; i++)
            {
                if (!saveObjects[i].LoadData())
                {
                    success = false;
                    Debug.Log("Loading failed of " + saveObjects[i].ErrorMessage);
                }
            }

            return success;
        }

        public bool Save()
        {
            bool success = true;
            foreach (ISavable obj in saveObjects)
            {
                if (!obj.SaveData())
                {
                    success = false;
                    Debug.Log("Saving failed of " + obj.ErrorMessage);
                }
            }

            return success;
        }

        public bool addSavable(ISavable obj)
        {
            if (saveObjects.Contains(obj))
            {
                Debug.Log("Trying to save the same obj" + obj + "twice");
                return false;
            }
            else
                saveObjects.Add(obj);

            return true;
        }
    }
}