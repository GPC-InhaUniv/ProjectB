using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectB.Characters.Players;
using ProjectB.UI.Presenter;

namespace ProjectB.GameManager
{
    public class GameMediator : Singleton<GameMediator>
    {
        GameObject playerObject;
      
        GameObject mainUICanvas;
        GameObject playerPresenter;

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


        }

        public void ClearStage()
        {
            uiController.EndStage();
        }

    }
}
