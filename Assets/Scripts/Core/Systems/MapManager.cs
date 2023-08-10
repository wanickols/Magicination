using System;
using System.Linq;
using UnityEngine;

namespace Core
{
    [Serializable]
    public class MapManager
    {

        private EncounterManager encounters;


        public Map map { get; private set; }

        public Grid grid => map.grid;

        // Start is called before the first frame update
        public MapManager(EncounterManager encounterManager, Map startingMap)
        {

            this.encounters = encounterManager;

            //Init Map
            map = GameObject.Instantiate(startingMap);
            GameObject.DontDestroyOnLoad(map);

        }


        public void LoadMap(Transfer trnsfer)
        {
            Map oldMap = map;

            if (oldMap.region != null)
                oldMap.region.TriggerBattle -= encounters.StartBattle;

            map = GameObject.Instantiate(trnsfer.NewMap);

            GameObject.Destroy(oldMap.gameObject);




            Transfer[] transfers = GameObject.FindObjectsOfType<Transfer>();

            Transfer _transfer = transfers.Where(transfer => trnsfer.isDestination(transfer)).FirstOrDefault();

            Game.manager.player.transform.position = grid.Center2D(_transfer.Cell + trnsfer.Offset);
            if (map.region != null)
                map.region.TriggerBattle += encounters.StartBattle;

        }




    }
}