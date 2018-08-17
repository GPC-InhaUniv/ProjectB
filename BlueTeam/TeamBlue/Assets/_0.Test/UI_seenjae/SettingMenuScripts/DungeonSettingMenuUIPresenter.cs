using UnityEngine;
using UnityEngine.UI;

namespace ProjectB.UI.SettingMenu
{
    public class DungeonSettingMenuUIPresenter : SettingMenuUI
    {

        [Header("DungeonOnly")]
        [SerializeField]
        GameObject messageWindowUI;
        [SerializeField]
        Button returnToVillageButton;
        [SerializeField]
        Button yesButton;
        [SerializeField]
        Button noButton;
        [SerializeField]
        Text messageText;
        
        void Start()
        {
            InActivateWindows(activatedMenu, settingWindowUI);
            InActivateWindows(activatedMenu, soundControlWindowUI);
            InActivateWindows(activatedMenu, messageWindowUI);

           
            settingButton.onClick.AddListener(delegate { ControlMenuWindow(activatedMenu, settingWindowUI, settingButton); });
            returnToGameButton.onClick.AddListener(delegate { ControlMenuWindow(activatedMenu, settingWindowUI, settingButton); });
            soundButton.onClick.AddListener(delegate { ControlWindows(activatedMenu, settingWindowUI, soundControlWindowUI); });
            returnToMenuButton.onClick.AddListener(delegate { ControlWindows(activatedMenu, soundControlWindowUI, settingWindowUI); });
            returnToVillageButton.onClick.AddListener(delegate { PopupMessage(activatedMenu, messageWindowUI, messageText, "마을로 돌아가시겠습니까?"); });
            noButton.onClick.AddListener(delegate { ControlWindows(activatedMenu, messageWindowUI, settingWindowUI); });
            yesButton.onClick.AddListener(delegate { ControlMenuWindow(activatedMenu, settingWindowUI, settingButton);
                ControlWindows(activatedMenu, messageWindowUI, messageWindowUI);
                ReturnToVillage();
            });
        }

        public void PopupMessage(bool ActivateMenu, GameObject popupWindowUI, Text text, string message)
        {
            popupWindowUI.SetActive(ActivateMenu);
            text.text = message;
        }

        public void ReturnToVillage()
        {
            Debug.Log("마을로 귀환성공");
        }
        
    }
}
