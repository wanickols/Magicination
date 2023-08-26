using System;
using System.Linq;
using UnityEngine;

namespace MGCNTN.Core
{
    [Serializable]
    public class MapManager
    {

        private EncounterManager encounters => Game.manager.encounterManager;


        public Map map { get; private set; }

        public Grid grid => map.grid;

        // Start is called before the first frame update
        public MapManager(Map startingMap)
        {
            //Init Map
            map = GameObject.Instantiate(startingMap);
        }


        public void LoadMap(Transfer trnsfer)
        {
            Map oldMap = map;

            if (oldMap.region != null)
                oldMap.region.TriggerBattle -= encounters.StartBattle;

            map = GameObject.Instantiate(trnsfer.NewMap);




            Transfer[] transfers = GameObject.FindObjectsOfType<Transfer>();

            Transfer _transfer = transfers.Where(transfer => transfer.isDestination(trnsfer)).FirstOrDefault();

            Game.manager.player.transform.position = grid.Center2D(grid.GetCell2D(_transfer.gameObject) + trnsfer.Offset);
            if (map.region != null)
                map.region.TriggerBattle += encounters.StartBattle;


            GameObject.Destroy(oldMap.gameObject);


        }




    }
}