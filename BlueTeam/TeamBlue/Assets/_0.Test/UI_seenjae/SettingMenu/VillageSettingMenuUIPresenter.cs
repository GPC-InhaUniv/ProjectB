
using UnityEngine;
using UnityEngine.UI;

namespace ProjectB.UI.SettingMenu
{
    //중복되는 부분이 많다. Abstract parent class를 만들어 관리하자
    public class VillageSettingMenuUIPresenter : MonoBehaviour
    {
        [SerializeField]
        Button settingButton;

        [Header("Windows")]
        [SerializeField]
        GameObject villageSettingMenu;
        [SerializeField]
        GameObject soundControlWindow;
       

        [Header("Buttons")]
        [SerializeField]
        Button soundButton;
        [SerializeField]
        Button returnToGameButton;
        [SerializeField]
        Button returnToMenuButton;
       
       // bool isGamePaused;
        bool activatedMenu;

        void Start()
        {
            villageSettingMenu.SetActive(false);
            soundControlWindow.SetActive(false);
            
            settingButton.onClick.AddListener(delegate { ControlMenu(activatedMenu, villageSettingMenu); });
            returnToGameButton.onClick.AddListener(delegate { ControlMenu(activatedMenu, villageSettingMenu); });
            soundButton.onClick.AddListener(delegate { ControlMenu(activatedMenu, villageSettingMenu, soundControlWindow); });
            returnToMenuButton.onClick.AddListener(delegate { ControlMenu(activatedMenu, villageSettingMenu, soundControlWindow); });
            
        }

        void RegistButtonListener(Button button)
        {
        }

        //setting메뉴 활성관리
        public void ControlMenu(bool ActivatedMenu, GameObject menuUI)
        {
            menuUI.SetActive(!ActivatedMenu);
            activatedMenu = !ActivatedMenu;
            settingButton.enabled = ActivatedMenu;
            PauseGame(activatedMenu);
        }

        //그 외 메뉴관련 팝업창 관리
        public void ControlMenu(bool ActivatedMenu, GameObject menuUI, GameObject popUI)
        {
            menuUI.SetActive(!ActivatedMenu);
            popUI.SetActive(ActivatedMenu);
            activatedMenu = !ActivatedMenu;
        }

        
        void PauseGame(bool ActivatedMenu)
        {
            switch(ActivatedMenu)
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
