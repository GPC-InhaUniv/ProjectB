using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using ProjectB.GameManager;
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
        GameObject worldMapUI;
        [SerializeField]
        GameObject resultUI;

        List<int> itemCode;
        string[] itemName;

        private void OnEnable()
        {
            itemCode = new List<int>();
            itemName = new string[2];
            ShowResultUI();
        }

        void ShowResultUI()
        {

            if (GameControllManager.Instance.IsClearDungeon)
            {
                resultText.text = "Stage Clear";
                getEXPText.text = "EXP : " + GameControllManager.Instance.TotalExp + "증가";
                foreach (KeyValuePair<int, int> temp in GameControllManager.Instance.ObtainedItemDic)
                {
                    itemCode.Add(temp.Key);
                }

                if (itemCode.Count != 0)
                {
                    for (int j = 0; j < itemCode.Count; j++)
                    {
                        for (int i = 0; i < itemTable.sheets[0].list.Count; i++)
                        {
                            if (itemCode[j] == itemTable.sheets[0].list[i].Code)
                            {
                                itemName[j] = itemTable.sheets[0].list[i].Name;
                                break;
                            }
                        }
                    }
                    StringBuilder stringBuilder = new StringBuilder();


                    for (int i = 0; i < itemName.Length; i++)
                    {
                        stringBuilder.Append(itemName[i] + "\n");
                    }

                    getItemText.text = "아이템 획득 목록 \n" + "\n" + stringBuilder.ToString();
                }
             
            }
            else
            {
                resultText.text = "Stage Fail";
                getEXPText.text = "EXP : " + GameControllManager.Instance.TotalExp + "감소";
                getItemText.text = string.Empty;
            }
            GameControllManager.Instance.ObtainedItemDic.Clear();

        }

        public void OnclickedStageButton()
        {
            resultUI.SetActive(false);
            worldMapUI.SetActive(true);


        }

        private void OnDisable()
        {
            itemCode = null;
        }

    }
}
