using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ProjectB.Characters;

namespace ProjectB.UI
{
    public class PlayerHUDPresenter : MonoBehaviour
    {
        [SerializeField]
        Character player;
        
        [SerializeField]
       private Image hPBar;
        [SerializeField]
        private Text levelText;

        private void Update()
        {
            ShowHUD();
        }
        public void ShowHUD()
        {
            hPBar.fillAmount = player.CharacterHealthPoint / player.CharacterMaxHealthPoint;
            levelText.text = "Level\n"+player.CharacterLevel.ToString();
        
        }


    }
}
