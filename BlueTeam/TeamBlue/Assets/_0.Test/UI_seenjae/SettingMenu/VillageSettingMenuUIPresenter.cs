
using UnityEngine;
using UnityEngine.UI;

namespace ProjectB.UI.SettingMenu
{
    public class VillageSettingMenuUIPresenter : SettingMenuUI
    {
        private void Start()
        {
            //리스트 적용 할 수 있는 방법이 없으려나
            inActivateWindows(activatedMenu, settingWindowUI);
            inActivateWindows(activatedMenu, soundControlWindowUI);

            registbuttonListener(activatedMenu, settingButton, settingWindowUI);
            registbuttonListener(activatedMenu, returnToGameButton, settingWindowUI);
            registbuttonListener(activatedMenu, soundButton, settingWindowUI, soundControlWindowUI);
            registbuttonListener(activatedMenu, returnToMenuButton, settingWindowUI, soundControlWindowUI);
        }
    }
}
