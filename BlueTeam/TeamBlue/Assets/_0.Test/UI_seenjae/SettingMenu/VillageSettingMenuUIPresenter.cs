
using UnityEngine;
using UnityEngine.UI;

namespace ProjectB.UI.SettingMenu
{
    public class VillageSettingMenuUIPresenter : SettingMenuUI
    {
        private void Start()
        {
            inActivateWindows(settingMenu, activatedMenu);
            inActivateWindows(soundControlWindow, activatedMenu);

            settingButton.onClick.AddListener(delegate { ControlWindows(activatedMenu, settingMenu); });
            returnToGameButton.onClick.AddListener(delegate { ControlWindows(activatedMenu, settingMenu); });
            soundButton.onClick.AddListener(delegate { ControlWindows(activatedMenu, settingMenu, soundControlWindow); });
            returnToMenuButton.onClick.AddListener(delegate { ControlWindows(activatedMenu, settingMenu, soundControlWindow); });
        }
    }
}
