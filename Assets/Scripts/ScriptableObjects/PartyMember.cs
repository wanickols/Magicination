using MGCNTN;
using MGCNTN.Core;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PartyMember : MemberBattleInfo
{
    public Stats stats;

    public Stats augmentStats;

    public Equipment equipment = new Equipment();

    public Stats Stats => stats + equipment.getEquipmentTotalStats() + augmentStats!;
    public void LoadFromFile(string filePath)
    {
        string[] jsons = File.ReadAllLines(filePath);
        Stats[] statsArr = new Stats[jsons.Length];

        int i = 0;
        foreach (string json in jsons)
            statsArr[i++] = JsonUtility.FromJson<Stats>(json);

        stats = statsArr[0];
        augmentStats = statsArr[1];

        //Load Equipment
    }

    public string SaveToFile()
    {
        string folderpath = "Playtime/PartyMembers/" + DisplayName + "/";
        string path = SaveManager.savePath + folderpath + DisplayName + ".json";

        List<string> jsons = new List<string>
        {
            JsonUtility.ToJson(stats),
            JsonUtility.ToJson(augmentStats)
        };

        //Save Equipment
        equipment.Save(folderpath);

        File.WriteAllLines(path, jsons);
        return path;
    }

}
