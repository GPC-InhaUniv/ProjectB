using UnityEngine;
using UnityEngine.UI;
using ProjectB.GameManager;

namespace ProjectB.UI.SettingMenu
{
    public class DungeonSettingMenuUIPresenter : SettingMenuUI
    {

        [Header("DungeonMenuOnly")]
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
            //창 비활성화
            InActivateWindows(isActivatingMenu, settingWindowUI);
            InActivateWindows(isActivatingMenu, soundControlWindowUI);
            InActivateWindows(isActivatingMenu, messageWindowUI);

            //버튼 등록
            settingButton.onClick.AddListener(delegate {
                ControlMenuWindow(isActivatingMenu, settingWindowUI, settingButton);
                PlaySound();
            });

            returnToGameButton.onClick.AddListener(delegate {
                ControlMenuWindow(isActivatingMenu, settingWindowUI, settingButton);
                PlaySound();
            });

            soundButton.onClick.AddListener(delegate {
                ControlWindows(isActivatingMenu, settingWindowUI, soundControlWindowUI);
                PlaySound();
            });

            returnToMenuButton.onClick.AddListener(delegate {
                ControlWindows(isActivatingMenu, soundControlWindowUI, settingWindowUI);
                PlaySound();
            });

            returnToVillageButton.onClick.AddListener(delegate {
                PopupMessage(isActivatingMenu, messageWindowUI, messageText, "마을로 돌아가시겠습니까?");
                ControlMenuWindow(isActivatingMenu, settingWindowUI);
                PlaySound();
            });

            noButton.onClick.AddListener(delegate {
                ControlWindows(isActivatingMenu, messageWindowUI, settingWindowUI);
                PlaySound();
            });

            yesButton.onClick.AddListener(delegate {
                ControlMenuWindow(isActivatingMenu, settingWindowUI, settingButton);
                ControlWindows(isActivatingMenu, messageWindowUI, messageWindowUI);
                PlaySound();
                ReturnToVillage();
            });

            //슬라이더 등록
            bgmVolumeSlier.onValueChanged.AddListener(delegate { ControlVolume(bgmVolumeSlier, sfxVolumeSlider); });
            sfxVolumeSlider.onValueChanged.AddListener(delegate { ControlVolume(bgmVolumeSlier, sfxVolumeSlider); });

            RegistComponent(bgmVolumeSlier);
            RegistComponent(sfxVolumeSlider);

            SetSliderValue(bgmVolumeSlier, minVolumeValue, maxVolumeValue);
            SetSliderValue(sfxVolumeSlider, minVolumeValue, maxVolumeValue);

            SetFirstSliderValue(bgmVolumeSlier, firstSliderValue);
            SetFirstSliderValue(sfxVolumeSlider, firstSliderValue);

        }

        public void PopupMessage(bool isActivatingMenu, GameObject popupWindowUI, Text text, string message)
        {
            popupWindowUI.SetActive(isActivatingMenu);
            text.text = message;
        }

        public void ReturnToVillage()
        {
            GameControllManager.Instance.MoveNextScene(LoadType.Village,0);
        }
        
    }
}
