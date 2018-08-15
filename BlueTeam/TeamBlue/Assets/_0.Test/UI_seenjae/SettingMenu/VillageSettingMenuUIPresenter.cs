
using UnityEngine;
using UnityEngine.UI;

namespace ProjectB.UI.SettingMenu
{
    public class VillageSettingMenuUIPresenter : SettingMenuUI
    {
        [SerializeField]
        Button settingButton;

        [Header("Windows")]
        [SerializeField]
        GameObject settingWindowUI;
        [SerializeField]
        GameObject soundControlWindowUI;


        [Header("Buttons")]
        [SerializeField]
        Button soundButton;
        [SerializeField]
        Button returnToGameButton;
        [SerializeField]
        Button returnToMenuButton;

        bool activatedMenu;

        private void Start()
        {
            
            InActivateWindows(activatedMenu, settingWindowUI);
            InActivateWindows(activatedMenu, soundControlWindowUI);

            settingButton.onClick.AddListener(delegate { ControlWindows(activatedMenu, settingWindowUI, settingButton); });
            returnToGameButton.onClick.AddListener(delegate { ControlWindows(activatedMenu, settingWindowUI, settingButton); });
            soundButton.onClick.AddListener(delegate { ControlWindows(activatedMenu, settingWindowUI, soundControlWindowUI); });
            returnToMenuButton.onClick.AddListener(delegate { ControlWindows(activatedMenu, settingWindowUI, soundControlWindowUI); });
        }
        
        protected override void InActivateWindows(bool ActivatedMenu, GameObject menuWindowUI)
        {
            menuWindowUI.SetActive(ActivatedMenu);
        }

       
        public override void ControlWindows(bool ActivatedMenu, GameObject menuWindowUI, Button button)
        {
            menuWindowUI.SetActive(!ActivatedMenu);
            activatedMenu = !ActivatedMenu;
            button.enabled = ActivatedMenu;
            PauseGame(activatedMenu);
            Debug.Log("윈도우 컨트롤러 호출됨: 마을");
            Debug.Log("마을: " + activatedMenu);
        }


        public override void ControlWindows(bool ActivatedMenu, GameObject menuWindowUI, GameObject popUPWindowUI)
        {
            menuWindowUI.SetActive(!ActivatedMenu);
            popUPWindowUI.SetActive(ActivatedMenu);
            activatedMenu = !ActivatedMenu;
        }

        protected override void PauseGame(bool ActivatedMenu)
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
