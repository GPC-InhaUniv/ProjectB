
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ProjectB.GameManager
{
    public class SceneController : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {
           Debug.Log("씬 판별 시작");
            GameControllManager.Instance.SetObjectPosition();
            GameControllManager.Instance.SetCameraPosition();
            GameMediator.Instance.GameInitialize();

        }


    }
}