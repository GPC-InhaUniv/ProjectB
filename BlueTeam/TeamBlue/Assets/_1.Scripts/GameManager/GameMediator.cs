using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectB.UI.Minimap;
using ProjectB.Characters.Players;
using ProjectB.UI.Presenter;

namespace ProjectB.GameManager
{
    public class GameMediator : Singleton<GameMediator>
    {
        GameObject playerObject;
      
        GameObject mainUICanvas;
        GameObject playerPresenter;
        GameObject minimapPresenter;

        IExitable uiController;

        public void SetUICanvas()
        {
            mainUICanvas = GameObject.FindGameObjectWithTag("UIController");
            mainUICanvas.SetActive(false);
        }

        public void SetMediator()
        {
            playerObject = GameObject.FindGameObjectWithTag("Player");
            
            playerPresenter = GameObject.FindGameObjectWithTag("PlayerPresenter");


            if (GameControllManager.Instance.CurrentLoadType != LoadType.Village &&
                  GameControllManager.Instance.CurrentLoadType != LoadType.VillageCheckDownLoad)
                minimapPresenter = GameObject.FindGameObjectWithTag("MinimapPresenter");
        }

        public void GameInitialize()
        {
            mainUICanvas.SetActive(true);
            SetMediator();
            
            IInitializable player = playerPresenter.GetComponent<IInitializable>();
            player.Initialize();
            player = playerObject.GetComponent<IInitializable>();
            player.Initialize();

            if (uiController == null)
                uiController = mainUICanvas.GetComponentInChildren<IExitable>();

            if (GameControllManager.Instance.CurrentLoadType != LoadType.Village &&
                 GameControllManager.Instance.CurrentLoadType != LoadType.VillageCheckDownLoad)
            {
                minimapPresenter.GetComponent<MinimapUIPresenter>().Initialize();
            }

        }

        public void ClearStage()
        {
            minimapPresenter.GetComponent<MinimapUIPresenter>().ResetRadar();
            uiController.EndStage();
        }

    }
}
