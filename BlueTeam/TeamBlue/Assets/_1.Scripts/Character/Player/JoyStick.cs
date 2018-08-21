using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ProjectB.UI
{
    public class JoyStick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
    {

        Image bgImg;
        Image joystickImg;
        Vector3 inputVector;

        void Start()
        {
            bgImg = GetComponent<Image>();
            joystickImg = transform.GetChild(0).GetComponent<Image>();
        }

        public virtual void OnDrag(PointerEventData OneventData)
        {
            Vector2 pos;
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(bgImg.rectTransform, OneventData.position, OneventData.pressEventCamera, out pos))
            {
                pos.x = (pos.x / bgImg.rectTransform.sizeDelta.x);
                pos.y = (pos.y / bgImg.rectTransform.sizeDelta.y);

                inputVector = new Vector3(pos.x, 0, pos.y); //x와 z값
                inputVector = (inputVector.magnitude > 1.0f) ? inputVector.normalized : inputVector;

                // Move JoyStick img
                joystickImg.rectTransform.anchoredPosition = new Vector3(inputVector.x * (bgImg.rectTransform.sizeDelta.x / 3), inputVector.z * (bgImg.rectTransform.sizeDelta.y / 3));
            }
        }

        public virtual void OnPointerUp(PointerEventData OneventData)
        {
            inputVector = Vector3.zero;
            joystickImg.rectTransform.anchoredPosition = Vector3.zero;
        }
        public virtual void OnPointerDown(PointerEventData OneventData)
        {
            OnDrag(OneventData);
        }


        public float Horizontal()
        {
            if (inputVector.x != 0)
                return inputVector.x;
            else
                return Input.GetAxisRaw("Horizontal");
        }
        public float Vertical()
        {
            if (inputVector.z != 0)
                return inputVector.z;
            else
                return Input.GetAxisRaw("Vertical");
        }
    }
}
