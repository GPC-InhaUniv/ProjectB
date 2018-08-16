
using UnityEngine;
using UnityEngine.UI;

namespace ProjectB.UI.SettingMenu
{
    public class VillageSettingMenuUIPresenter : SettingMenuUI
    {
        private void Start()
        {
            InActivateWindows(activatedMenu, settingWindowUI);
            InActivateWindows(activatedMenu, soundControlWindowUI);

            settingButton.onClick.AddListener(delegate { ControlMenuWindow(activatedMenu, settingWindowUI, settingButton); });
            returnToGameButton.onClick.AddListener(delegate { ControlMenuWindow(activatedMenu, settingWindowUI, settingButton); });
            soundButton.onClick.AddListener(delegate { ControlWindows(activatedMenu, settingWindowUI, soundControlWindowUI); });
            returnToMenuButton.onClick.AddListener(delegate { ControlWindows(activatedMenu, soundControlWindowUI, settingWindowUI); });
        }
    }
}
