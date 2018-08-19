using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectB.UI.Presenter
{
    public class StageResultPresenter : MonoBehaviour
    {
        [SerializeField]
        Text resultText;
        [SerializeField]
        Text getEXPText;
        [SerializeField]
        Text getItemText;
        [SerializeField]
        ItemTable itemTable;

        [SerializeField]
        bool isClear;
        [SerializeField]
        float EXP;
        [SerializeField]
        string item1;
        [SerializeField]
        string item2;

        private void OnEnable()
        {
            ShowResultUI();
        }

        void ShowResultUI()
        {
            if(isClear)
            {
                resultText.text = "Stage Clear";
                getEXPText.text = "EXP : " + EXP.ToString() + "증가";
                getItemText.text = "아이템 획득 목록 \n" + item1 + "\n" + item2;
            }
            else
            {
                resultText.text = "Stage Fail";
                getEXPText.text = "EXP : " + EXP.ToString() + "감소";
                getItemText.text = string.Empty;
            }
           
        }

        
    }
}
