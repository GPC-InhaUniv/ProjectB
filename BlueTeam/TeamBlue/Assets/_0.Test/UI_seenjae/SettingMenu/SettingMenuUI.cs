using UnityEngine;
using UnityEngine.UI;

namespace ProjectB.UI.SettingMenu
{
    public abstract class SettingMenuUI : MonoBehaviour
    {
        protected abstract void InActivateWindows(bool ActivatedMenu, GameObject menuWindowUI);
        
        public abstract void ControlWindows(bool ActivatedMenu, GameObject menuWindowUI, Button button);
        
        public abstract void ControlWindows(bool ActivatedMenu, GameObject menuWindowUI, GameObject popUPWindowUI);
        
        protected abstract void PauseGame(bool ActivatedMenu);
       
    }
}
