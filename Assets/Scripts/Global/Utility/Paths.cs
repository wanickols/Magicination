namespace MGCNTN
{
    public class Paths
    {
        public static string enemyDataPath = "ScriptableObjects/Enemies/EnemyData/";
        public static string enemyPackPath = "ScriptableObjects/Enemies/EnemyPacks/";
        public static string partyMemberPath = "ScriptableObjects/PartyMembers/";

        //Enemy Data
        public static string floatingEye = enemyDataPath + "FloatingEye";


        //Enemy Packs
        public static string TwoEyes = enemyPackPath + "TwoFloatingEye";


        public static string getCharacterPath(PlayableCharacters character)
        {
            return character switch
            {
                PlayableCharacters.Aurora => partyMemberPath + "Aurora",
                PlayableCharacters.Leon => partyMemberPath + "Leon",
                PlayableCharacters.Seth => partyMemberPath + "Seth",
                PlayableCharacters.Zane => partyMemberPath + "Zane",
            };
        }



        //other
        public static string BattleTransition = "BattlePrefabs/BattleTransition";
    }
}