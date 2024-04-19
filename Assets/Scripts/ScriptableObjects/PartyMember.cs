using MGCNTN;
using MGCNTN.Core;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class PartyMember : MemberBattleInfo
{
    [SerializeField] private Stats stats;

    public Equipment equipment = new Equipment();

    string folderpath => "Playtime/PartyMembers/" + DisplayName + "/";

    public override Stats baseStats { get => stats; set => stats = value; }

    public Stats CombinedStats => baseStats + equipment.getEquipmentTotalStats();
    public void LoadFromFile(string filePath)
    {

        string[] jsons = File.ReadAllLines(filePath);
        int jsonLength = jsons.Length;

        // Load playableCharacter data
        playableCharacter = JsonUtility.FromJson<PlayableCharacters>(jsons[0]);

        // Load baseStats data
        baseStats = JsonUtility.FromJson<Stats>(jsons[1]);

        // Load augmentingStats data
        augmentingStats.Clear(); // Clear augmentingStats list to avoid duplicates
        for (int i = 2; i < jsonLength; i++)
        {
            AugmentStats augmentStats = JsonUtility.FromJson<AugmentStats>(jsons[i]);
            augmentingStats.Add(augmentStats);
        }

        //Load Equipment
        filePath = SaveManager.savePath + folderpath + "Equipment.json";
        List<string> paths = File.ReadAllLines(filePath).ToList();

        equipment.loadEquipmentFromList(paths);
    }

    public string SaveToFile()
    {
        string path = SaveManager.savePath + folderpath + DisplayName + ".json";



        ///Base Stats and Name
        List<string> jsons = new List<string>
        {
            JsonUtility.ToJson(playableCharacter),
            JsonUtility.ToJson(baseStats),

        };

        ///Augmenting Stats
        foreach (Stats stats in augmentingStats)
        {
            jsons.Add(JsonUtility.ToJson(stats));
        }

        //Save Equipment
        File.WriteAllLines(SaveManager.savePath + folderpath + "Equipment.json", equipment.getSaveList());


        File.WriteAllLines(path, jsons);
        return path;
    }

}
