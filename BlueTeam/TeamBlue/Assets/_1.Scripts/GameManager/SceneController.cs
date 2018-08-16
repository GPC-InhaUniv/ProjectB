
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ProjectB.GameManager
{
    public class SceneController : MonoBehaviour,IPointerClickHandler
    {

        [SerializeField]
        GameObject panel;


        // Use this for initialization
        void Start()
        {
            Debug.Log("씬 판별 시작");
            GameControllManager.Instance.SetObjectPosition();
            GameControllManager.Instance.SetUI();
            GameControllManager.Instance.SetCameraPosition();

        }

        public void OnClickWorldMapBtn()
        {
            if (!panel.activeInHierarchy)
                panel.SetActive(true);
            else panel.SetActive(false);

        }

        public void OnClickDungeonBtn(int index)
        {
            Debug.Log("던전 입장");
            GameControllManager.Instance.MoveNextScene(LoadType.WoodDungeon, index);
        }

        public void OnClickSheepBtn(int index)
        {
            GameControllManager.Instance.MoveNextScene(LoadType.SheepDungeon, index);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            Debug.Log(eventData.pointerCurrentRaycast.gameObject);
            
          
        }
    }
}