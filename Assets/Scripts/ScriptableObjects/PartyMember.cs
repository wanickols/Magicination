using MGCNTN;
using MGCNTN.Core;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class PartyMember : MemberBattleInfo
{
    public Stats stats;

    public Stats augmentStats;

    public Equipment equipment = new Equipment();

    string folderpath => "Playtime/PartyMembers/" + DisplayName + "/";

    public Stats Stats => stats + equipment.getEquipmentTotalStats() + augmentStats!;
    public void LoadFromFile(string filePath)
    {

        string[] jsons = File.ReadAllLines(filePath);
        int jsonLength = jsons.Length;

        Stats[] statsArr = new Stats[jsonLength];

        playableCharacter = JsonUtility.FromJson<PlayableCharacters>(jsons[0]);


        for (int i = 1; i < jsonLength; i++)
        {
            statsArr[i - 1] = JsonUtility.FromJson<Stats>(jsons[i]);
        }


        stats = new Stats();
        stats = statsArr[0];

        if (jsonLength > 1)
            augmentStats = statsArr[1];

        //Load Equipment
        filePath = SaveManager.savePath + folderpath + "Equipment.json";
        List<string> paths = File.ReadAllLines(filePath).ToList();

        equipment.loadEquipmentFromList(paths);
    }

    public string SaveToFile()
    {
        string path = SaveManager.savePath + folderpath + DisplayName + ".json";




        List<string> jsons = new List<string>
        {
            JsonUtility.ToJson(playableCharacter),
            JsonUtility.ToJson(stats),
            JsonUtility.ToJson(augmentStats)
        };

        //Save Equipment
        File.WriteAllLines(SaveManager.savePath + folderpath + "Equipment.json", equipment.getSaveList());


        File.WriteAllLines(path, jsons);
        return path;
    }

}
