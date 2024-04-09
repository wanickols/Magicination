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
    }

    public string SaveToFile()
    {
        string folderpath = "Playtime/PartyMembers/" + DisplayName + "/";
        string path = SaveManager.savePath + folderpath + DisplayName + ".json";

        List<string> jsons = new List<string>
        {
            JsonUtility.ToJson(playableCharacter),
            JsonUtility.ToJson(stats),
            JsonUtility.ToJson(augmentStats)
        };

        //Save Equipment
        equipment.Save(folderpath);

        File.WriteAllLines(path, jsons);
        return path;
    }

}
