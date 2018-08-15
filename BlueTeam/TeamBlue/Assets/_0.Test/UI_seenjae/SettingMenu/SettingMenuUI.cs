using UnityEngine;
using UnityEngine.UI;

namespace ProjectB.UI.SettingMenu
{
    public abstract class SettingMenuUI : MonoBehaviour
    {
        [SerializeField]
        protected Button settingButton;

        [Header("Windows")]
        [SerializeField]
        protected GameObject settingMenu;
        [SerializeField]
        protected GameObject soundControlWindow;
        

        [Header("Buttons")]
        [SerializeField]
        protected Button soundButton;
        [SerializeField]
        protected Button returnToGameButton;
        [SerializeField]
        protected Button returnToMenuButton;

        protected bool activatedMenu;
        
        //setting메뉴 활성관리
        protected void inActivateWindows(GameObject menuUI, bool activatedMenu)
        {
            menuUI.SetActive(activatedMenu);
        }

        public void ControlWindows(bool ActivatedMenu, GameObject menuUI)
        {
            menuUI.SetActive(!ActivatedMenu);
            activatedMenu = !ActivatedMenu;
            settingButton.enabled = ActivatedMenu;
            PauseGame(activatedMenu);
        }

        //그 외 메뉴관련 팝업창 관리
        public void ControlWindows(bool ActivatedMenu, GameObject menuUI, GameObject popUI)
        {
            menuUI.SetActive(!ActivatedMenu);
            popUI.SetActive(ActivatedMenu);
            activatedMenu = !ActivatedMenu;
        }

        protected void PauseGame(bool ActivatedMenu)
        {
            switch (ActivatedMenu)
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
