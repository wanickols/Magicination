using System;
using UnityEngine;

namespace MGCNTN.Core
{
    [Serializable]
    public class EncounterManager
    {
        private StateManager stateManager;
        private MapManager mapManager;
        private Map map => mapManager.map;
        private SceneLoader sceneLoader;

        [SerializeField] private GameObject transitionPrefab;


        public EncounterManager() { }

        public void init(StateManager stateManager, SceneLoader sceneLoader, MapManager mapManager)
        {
            this.stateManager = stateManager;
            this.mapManager = mapManager;
            this.sceneLoader = sceneLoader;

            if (map.region != null)
                map.region.TriggerBattle += StartBattle;
        }
        /*
                public void debugUpdate()
                {
                    if (Input.GetKeyDown(KeyCode.E))
                        EndBattle();
                }*/
        public void StartBattle()
        {

            if (!stateManager.tryState(GameState.Battle))
                return;

            Battle.Battle.currentRegion = map.region;
            Battle.Battle.endBattle += EndBattle;

            Game.manager.StartCoroutine(sceneLoader.Co_loadScene(SceneLoader.scene.battle, transitionPrefab, map));
            map.gameObject.SetActive(false);

        }

        public void EndBattle()
        {
            if (!stateManager.tryState(GameState.World))
                return;


            map.gameObject.SetActive(true);
            sceneLoader.loadScene(SceneLoader.savedScene, map);

            Battle.Battle.endBattle -= EndBattle;

        }

        ~EncounterManager()
        {
            if (map.region != null)
                map.region.TriggerBattle -= StartBattle;
        }

    }

}