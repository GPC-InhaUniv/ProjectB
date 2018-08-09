using System.Collections.Generic;
using System.IO;
using UnityEngine;
using ProjectB.GameManager;

namespace ProjectB.Utility
{
    public class JsonManager : MonoBehaviour
    {
        string assetBundleDirectory;

        void Start()
        {

#if UNITY_ANDROID
            assetBundleDirectory = Application.persistentDataPath + "/JsonFolder";
#else
        assetBundleDirectory="Assets/10.JsonFolder";
#endif
        }



        //제이슨 파일 저장
        public void OnClickSaveJSONBtn()
        {
            Data mydata = new Data();
            string save = JsonUtility.ToJson(mydata, prettyPrint: true);
            Debug.Log(save);
            WriteStringToFile(save, "save.json");

        }


        //제이슨 파일 로드
        public void OnClickLoadJSONBtn()
        {
            string load = ReadStringFromFile("save.json");
            var loadData = JsonUtility.FromJson<Data>(load);
            Debug.Log(loadData);
        }



        string ReadStringFromFile(string path)
        {
            string text = System.IO.File.ReadAllText(assetBundleDirectory + "/" + path);

            return text;
        }


        void WriteStringToFile(string text, string path)
        {
            // 에셋 번들을 저장할 경로의 폴더가 존재하지 않는다면 생성시킨다.
            if (!Directory.Exists(assetBundleDirectory))
            {
                Directory.CreateDirectory(assetBundleDirectory);
            }

            using (System.IO.StreamWriter file =
                 new System.IO.StreamWriter(assetBundleDirectory + "/" + path, false))
            {
                file.WriteLine(text);
            }

        }

    }


    [SerializeField]
    public class Data
    {
        public PlayerInformation PlayerInfomation;
        public TownInformation ATownInformation;
        public TownInformation BTownInformation;
        public PlayerInventoryData PlayerInventoryData;
        public PlayerWareHouseData PlayerWareHouseData;

        public Data()
        {
            PlayerInfomation = GameDataManager.Instance.PlayerInfomation;
            ATownInformation = GameDataManager.Instance.AtownInformation;
            BTownInformation = GameDataManager.Instance.BtownInformation;
            PlayerInventoryData.Code = new int[GameDataManager.Instance.PlayerGamedata.Count];
            PlayerInventoryData.Count = new int[GameDataManager.Instance.PlayerGamedata.Count];
            PlayerWareHouseData.Code = new int[GameDataManager.Instance.WareHouseGamedata.Count];
            PlayerWareHouseData.Count = new int[GameDataManager.Instance.WareHouseGamedata.Count];

            int tempIndex = 0;
            foreach (KeyValuePair<int, int> temp in GameDataManager.Instance.PlayerGamedata)
            {
                PlayerInventoryData.Code[tempIndex] = temp.Key;
                PlayerInventoryData.Count[tempIndex] = temp.Value;
                tempIndex++;


            }
            tempIndex = 0;
            foreach (KeyValuePair<int, int> temp in GameDataManager.Instance.WareHouseGamedata)
            {
                PlayerWareHouseData.Code[tempIndex] = temp.Key;
                PlayerWareHouseData.Count[tempIndex] = temp.Value;
                tempIndex++;


            }

        }
    }
}





