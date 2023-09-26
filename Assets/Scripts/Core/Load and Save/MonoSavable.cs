using MGCNTN.Core;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
/// <summary>
/// Yes this is redundant. I couldn't find a better idea to add the monobehavior
/// </summary>
public abstract class MonoSavable : MonoBehaviour, ISavable
{

    private void Awake()
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
    protected string[] loadFromFile() => File.ReadAllLines(path);

    bool ISavable.SaveData() => this.SaveData();

    bool ISavable.LoadData() => this.LoadData();
}
