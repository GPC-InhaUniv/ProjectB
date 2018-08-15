using UnityEngine;
using UnityEngine.UI;
//좀더 구조화를 할 수 없을까 중복되는부분이 많다.
namespace ProjectB.UI.SettingMenu
{
    public class DungeonSettingMenuUIPresenter : SettingMenuUI
    {
        [Header("Windows")]
        [SerializeField]
        GameObject messageWindow;
        
        bool isGamePaused;

        void Start()
        {
            settingMenu.SetActive(false);
            soundControlWindow.SetActive(false);
            messageWindow.SetActive(false);
        }

        public void PopWindow()
        {

        }

        public void PullWindow()
        {

        }
    }
}
