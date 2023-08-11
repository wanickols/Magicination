using System;
using UnityEngine;

namespace Core
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

        public void debugUpdate()
        {
            if (Input.GetKeyDown(KeyCode.E))
                EndBattle();
        }

        //TODO: move this to scene loader, can use event
        public void StartBattle()
        {

            stateManager.changeState(GameState.Battle);
            Battle.Battle.currentRegion = map.region;
            Battle.Battle.endBattle += EndBattle;

            Game.manager.StartCoroutine(sceneLoader.Co_loadScene(SceneLoader.scene.battle, transitionPrefab, map));
            map.gameObject.SetActive(false);

        }

        public void EndBattle()
        {
            if (stateManager.State == GameState.Battle)
            {
                map.gameObject.SetActive(true);
                sceneLoader.loadScene(SceneLoader.savedScene, map);

                stateManager.changeState(GameState.World);
                Battle.Battle.endBattle -= EndBattle;
            }
        }

        ~EncounterManager()
        {
            if (map.region != null)
                map.region.TriggerBattle -= StartBattle;
        }

    }

}