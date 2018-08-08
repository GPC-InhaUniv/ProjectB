using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//public struct OwnerObjectStruct
//{
//    public Image Dot;
//    public GameObject Character;
//}
public class RadarUIPresenter : MonoBehaviour //인터페이스 구현
{
    [SerializeField]
    Radar radar;
    [SerializeField]
    Image Dot;
    [SerializeField]
    GameObject Character;

    void Start()
    {
        radar.RegistIcon(Character, Dot);
    }

    //private void OnEnable()
    //{
    //    radar.RegistIcon(Character, Dot);
    //}

    private void LateUpdate()
    {
        if(Character.transform.position == null)
        {
            requestRemove();
        }
    }

    void SetDotImage()
    {

    }

    void requestRemove()
    {
        radar.RemoveIcon(Character);
    }

    //인터페이스 메소드 구현부
    //void InterfaceMethod(Vector3 vector3)
    //{
    //    Character.transform.position = vector3;
    //}

}
