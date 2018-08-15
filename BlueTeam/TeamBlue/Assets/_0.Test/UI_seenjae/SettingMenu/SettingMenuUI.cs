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

        protected bool activatedMenu;
        
        //setting메뉴 활성관리
        protected void inActivateWindows(bool ActivatedMenu, GameObject menuWindowUI)
        {
            menuWindowUI.SetActive(activatedMenu);
        }
        
        protected void registbuttonListener(bool ActivatedMenu, Button button, GameObject menuWindowUI)
        {
             button.onClick.AddListener(delegate { ControlWindows(activatedMenu, menuWindowUI); });
        }

        protected void registbuttonListener(bool ActivatedMenu, Button button, GameObject menuWindowUI, GameObject popUpWidnowUI)
        {
            button.onClick.AddListener(delegate { ControlWindows(activatedMenu, menuWindowUI, popUpWidnowUI); });
        }

        public void ControlWindows(bool ActivatedMenu, GameObject menuWindowUI)
        {
            menuWindowUI.SetActive(!ActivatedMenu);
            activatedMenu = !ActivatedMenu;
            settingButton.enabled = ActivatedMenu;
            PauseGame(activatedMenu);
            Debug.Log("윈도우 컨트롤러 호출됨");
        }

        //그 외 메뉴관련 팝업창 관리
        public void ControlWindows(bool ActivatedMenu, GameObject menuWindowUI, GameObject popUPWindowUI)
        {
            menuWindowUI.SetActive(!ActivatedMenu);
            popUPWindowUI.SetActive(ActivatedMenu);
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
