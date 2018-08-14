
using UnityEngine;
using UnityEngine.UI;

namespace ProjectB.UI.SettingMenu
{
    //이 프레젠터는 각각 큰 패널들만 관리한다.
    public class SettingMenuUIPresenter : MonoBehaviour
    {
        [SerializeField]
        Button settingButton;

        [Header("Panels")]
        [SerializeField]
        GameObject villageSettingMenu;
        [SerializeField]
        GameObject dungeonSettingMenu;
        [SerializeField]
        GameObject soundControllWindow;
        [SerializeField]
        GameObject messageWindow;

        //이 프레젠터에서 모든 메뉴의 버튼을 관리해야 될 때 다시 활성화
        //[Header("Buttons")]
        //[SerializeField]
        //Button soundButton;
        //[SerializeField]
        //Button restartButton;
        //[SerializeField]
        //Button returnToVillageButton;
        //[SerializeField]
        //Button reternButton;
        

        bool isPaused;

        void Start()
        {
            settingButton = settingButton.GetComponent<Button>();
            villageSettingMenu.SetActive(false);
            dungeonSettingMenu.SetActive(false);
            soundControllWindow.SetActive(false);
            messageWindow.SetActive(false);

            
        }

        //던전메뉴와 마을메뉴가 달라야함
        //하지만 씬은 하나임
       
        void Update()
        {
            //village 필드가 활성이 되어 있다면 (조건)
            //setting button클릭했을 때 AddListener가 계속호출되는 문제
            //https://www.youtube.com/watch?v=JivuXdrIHK0 UI 팝업, 패널관련 영상
            settingButton.onClick.AddListener(delegate { popSettingMenu(isPaused, villageSettingMenu); } );
        }
        
        //SettingButton의 OnClick으로 들어가는 메소드
        //인스펙터에서 OnClick으로 넣어주느냐 스크립트에서 처리하느냐 결정해야 함
        public void popSettingMenu(bool isPaused, GameObject MenuUI)
        {
            villageSettingMenu.SetActive(true);
            Time.timeScale = 0f;
            isPaused = true;
            Debug.Log(Time.timeScale);
        }
        
        public void pullSettingMenu(bool isPaused, GameObject MenuUI)
        {
            villageSettingMenu.SetActive(false);
            Time.timeScale = 1f;
            isPaused = false;
            Debug.Log(Time.timeScale);
        }

    }
}
