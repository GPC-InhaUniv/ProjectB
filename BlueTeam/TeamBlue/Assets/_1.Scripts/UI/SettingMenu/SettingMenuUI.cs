using UnityEngine;
using UnityEngine.UI;
using ProjectB.GameManager;

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
       
        [Header("Sliders")]
        [SerializeField]
        protected Slider bgmVolumeSlier;
        [SerializeField]
        protected Slider sfxVolumeSlider;

        
        protected bool isActivatingMenu;
        protected const float firstSliderValue = 1.0f;
        protected const float minVolumeValue = 0.0f;
        protected const float maxVolumeValue = 1.0f;
        

        protected void RegistComponent(Slider volumeSlider)
        {
            volumeSlider = GetComponent<Slider>();
        }

        protected void SetSliderValue(Slider volumeSlider, float minValue, float maxValue)
        {
            volumeSlider.minValue = minValue;
            volumeSlider.maxValue = maxValue;
        }

        protected void SetFirstSliderValue(Slider volumeSlider, float firstValue)
        {
            volumeSlider.value = firstValue;
        }

        protected void ControlVolume(Slider bgm, Slider sfx)
        {
            SoundManager.Instance.SetVolume(bgm.value, sfx.value);
        }
        
        protected void InActivateWindows(bool isActivatingMenu, GameObject menuWindowUI)
        {
            menuWindowUI.SetActive(isActivatingMenu);
        }
        
        public void ControlMenuWindow(bool isActivatingMenu, GameObject menuWindowUI, Button button)
        {
            menuWindowUI.SetActive(!isActivatingMenu);
            this.isActivatingMenu = !isActivatingMenu;
            button.enabled = isActivatingMenu;
            PauseGame(this.isActivatingMenu);
        }

        public void ControlWindows(bool isActivatingMenu, GameObject menuWindowUI, GameObject popUPWindowUI)
        {
            menuWindowUI.SetActive(!isActivatingMenu);
            popUPWindowUI.SetActive(isActivatingMenu);
        }

        protected void PauseGame(bool isActivatingMenu)
        {
            switch (isActivatingMenu)
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
