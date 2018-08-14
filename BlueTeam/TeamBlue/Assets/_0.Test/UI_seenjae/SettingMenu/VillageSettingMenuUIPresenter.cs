
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;
namespace ProjectB.UI.SettingMenu
{
    //이 프레젠터는 각각 큰 패널들만 관리한다.
    public class VillageSettingMenuUIPresenter : MonoBehaviour
    {
        [SerializeField]
        Button settingButton;

        [Header("Panels")]
        [SerializeField]
        GameObject villageSettingMenu;

        [SerializeField]
        GameObject soundControllWindow;
        [SerializeField]
        GameObject messageWindow;



        [Header("Buttons")]
        [SerializeField]
        Button soundButton;
        //[SerializeField]
        //Button restartButton;
        //[SerializeField]
        //Button returnToVillageButton;
        [SerializeField]
        Button returnToGameButton;
        [SerializeField]
        Button returnButton;


        bool isGamePaused;

        void Start()
        {
            villageSettingMenu.SetActive(false);
            soundControllWindow.SetActive(false);
            messageWindow.SetActive(false);

            settingButton.onClick.AddListener(delegate { PopMenu(isGamePaused, villageSettingMenu); });
            returnToGameButton.onClick.AddListener(delegate { PullMenu(isGamePaused, villageSettingMenu); });
            soundButton.onClick.AddListener(delegate { PopMenu(villageSettingMenu, soundControllWindow); });
            returnButton.onClick.AddListener(delegate { PullMenu(villageSettingMenu, soundControllWindow); });
        }

        
        public void PopMenu(bool IsGamePaused, GameObject menuUI)
        {
            menuUI.SetActive(true);
            isGamePaused = !IsGamePaused;
            settingButton.enabled = IsGamePaused;
            PauseGame(IsGamePaused);
        }

        public void PopMenu(GameObject menuUI, GameObject popUI)
        {
            menuUI.SetActive(false);
            popUI.SetActive(true);
        }
        
        public void PullMenu(bool IsGamePaused, GameObject menuUI)
        {
            menuUI.SetActive(false);
            isGamePaused = !IsGamePaused;
            settingButton.enabled = IsGamePaused;
            PauseGame(IsGamePaused);
        }

        public void PullMenu(GameObject menuUI, GameObject pullUI)
        {
            menuUI.SetActive(true);
            pullUI.SetActive(false);
        }

        void PauseGame(bool isGamePaused)
        {
            switch(isGamePaused)
            {
                case true:
                    Time.timeScale = 0.0f;
                    break;

                case false:
                    Time.timeScale = 1.0f;
                    break;
            }
        }
        
    }
}
