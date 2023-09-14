using System.Collections.Generic;
using UnityEngine;

namespace MGCNTN.Core
{
    public class SaveManager
    {
        public static string savePath = "Assets/Saves/";

        private List<Savable> saveObjects = new List<Savable>();

        public bool Load()
        {
            bool success = true;
            foreach (Savable obj in saveObjects)
            {
                if (!obj.LoadData())
                {
                    success = false;
                    Debug.Log("Saving failed of " + obj.errorMessage);
                }
            }

            return success;
        }

        public bool Save()
        {
            bool success = true;
            foreach (Savable obj in saveObjects)
            {
                if (!obj.SaveData())
                {
                    success = false;
                    Debug.Log("Saving failed of " + obj.errorMessage);
                }
            }

            return success;
        }

        public bool addSavable(Savable obj)
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