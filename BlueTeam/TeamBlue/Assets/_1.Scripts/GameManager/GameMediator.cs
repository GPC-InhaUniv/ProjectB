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


        public void SetMediator()
        {
            playerObject = GameObject.FindGameObjectWithTag("Player");
            mainUICanvas = GameObject.FindGameObjectWithTag("UIController");
            mainUICanvas.SetActive(false);
        }

        public void GameInitialize()
        {
            IInitializable player = playerObject.GetComponent<IInitializable>();
            player.Iniitialize();
        }
        

    }
}
