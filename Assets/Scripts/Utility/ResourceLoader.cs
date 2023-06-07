using UnityEngine;

public class ResourceLoader
{
    public static string enemyDataPath = "ScriptableObjects/Enemies/EnemyData/";
    public static string enemyPackPath = "ScriptableObjects/Enemies/EnemyPacks/";
    public static string partyMemberPath = "ScriptableObjects/PartyMembers/";

    //Enemy Data
    public static string floatingEye = enemyDataPath + "FloatingEye";


    //Enemy Packs
    public static string TwoEyes = enemyPackPath + "TwoFloatingEye";

    //Party Members
    public static string Aaron = partyMemberPath + "Aaron";
    public static string Kaja = partyMemberPath + "Kaja";
    public static string Seth = partyMemberPath + "Seth";
    public static string Zera = partyMemberPath + "Zera";


    //other
    public static string BattleTransition = "BattlePrefabs/BattleTransition";

    public static T Load<T>(string resource) where T : Object
    {
        T loadedItem = Resources.Load<T>(resource);

        if (loadedItem != null)
        {
            return loadedItem;
        }

        Debug.LogError($"Could not load {resource}");
        return null;

    }
}
