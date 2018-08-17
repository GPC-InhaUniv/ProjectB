using UnityEngine;
using UnityEngine.UI;

namespace ProjectB.UI.SettingMenu
{
    public class SettingMenuUI : MonoBehaviour
    {
        [SerializeField]
        protected Button settingButton;

        [Header("Windows")]
        [SerializeField]
        protected GameObject settingWindowUI;
        [SerializeField]
        protected GameObject soundControlWindowUI;


        [Header("Buttons")]
        [SerializeField]
        protected Button soundButton;
        [SerializeField]
        protected Button returnToGameButton;
        [SerializeField]
        protected Button returnToMenuButton;
        //사운드조절 슬라이더 추가하기

        protected bool activatedMenu;
        
        protected void InActivateWindows(bool ActivatedMenu, GameObject menuWindowUI)
        {
            menuWindowUI.SetActive(ActivatedMenu);
        }
        
        public void ControlMenuWindow(bool ActivatedMenu, GameObject menuWindowUI, Button button)
        {
            menuWindowUI.SetActive(!ActivatedMenu);
            activatedMenu = !ActivatedMenu;
            button.enabled = ActivatedMenu;
            PauseGame(activatedMenu);
        }

        public void ControlWindows(bool ActivatedMenu, GameObject menuWindowUI, GameObject popUPWindowUI)
        {
            menuWindowUI.SetActive(!ActivatedMenu);
            popUPWindowUI.SetActive(ActivatedMenu);
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
