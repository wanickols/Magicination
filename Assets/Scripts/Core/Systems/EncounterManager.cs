using System;
using UnityEngine;

namespace MGCNTN.Core
{
    [Serializable]
    public class EncounterManager
    {
        private readonly Guid debugId = Guid.NewGuid();
        private StateManager stateManager;
        private MapManager mapManager;
        private Map map => mapManager.map;
        private SceneLoader sceneLoader;


        [SerializeField] private GameObject transitionPrefab;
        private Region subscribedRegion;

        public EncounterManager() { }

        public void init(StateManager stateManager, SceneLoader sceneLoader, MapManager mapManager)
        {
            this.stateManager = stateManager;
            this.mapManager = mapManager;
            this.sceneLoader = sceneLoader;

            SubscribeToRegion(map.region);
        }
      

        public void Shutdown() => UnsubscribeFromRegion();

       
        public void StartBattle()
        {
           Debug.Log(
               $"StartBattle: EncounterManager ID={debugId}, " +
               $"Map={(map ? map.GetInstanceID().ToString() : "NULL")}"
           );


            if (!map) return;

            if (!stateManager.tryState(GameState.Battle))
                return;

            Battle.Battle.currentRegion = map.region;
            Battle.Battle.endBattle += EndBattle;

            if (map.region != null)
                map.region.TriggerBattle -= StartBattle;

            Game.manager.StartCoroutine(sceneLoader.Co_loadScene(SceneLoader.scene.battle, transitionPrefab, map));
            map.gameObject.SetActive(false);

        }

        public void EndBattle()
        {
            if (!stateManager.tryState(GameState.World))
                return;

            if (map.region != null)
                map.region.TriggerBattle += StartBattle;

            map.gameObject.SetActive(true);
            sceneLoader.loadScene(SceneLoader.savedScene, map);

            Battle.Battle.endBattle -= EndBattle;

        }

        //private
        private void SubscribeToRegion(Region region)
        {
            UnsubscribeFromRegion();

            if (region == null)
                return;

            subscribedRegion = region;
            subscribedRegion.TriggerBattle += StartBattle;
        }
        private void UnsubscribeFromRegion()
        {
            if (subscribedRegion == null)
                return;

            subscribedRegion.TriggerBattle -= StartBattle;
            subscribedRegion = null;
        }

    }

}