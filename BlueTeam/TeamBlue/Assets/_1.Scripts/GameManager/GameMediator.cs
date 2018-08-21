using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectB.Characters.Players;

namespace ProjectB.GameManager
{
    public class GameMediator : Singleton<GameMediator>
    {
        GameObject playerObject;
        GameObject mainUICanvas;
        GameObject playerPresenter;

        public void SetMediator()
        {
            playerObject = GameObject.FindGameObjectWithTag("Player");
            mainUICanvas = GameObject.FindGameObjectWithTag("UIController");
            playerPresenter = GameObject.FindGameObjectWithTag("PlayerPresenter");
           
        }

        public void GameInitialize()
        {
            SetMediator();
            IInitializable player = playerPresenter.GetComponent<IInitializable>();
            player.Initialize();
            player = playerObject.GetComponent<IInitializable>();
            player.Initialize();
           
            mainUICanvas.SetActive(false);
        }
        

    }
}
