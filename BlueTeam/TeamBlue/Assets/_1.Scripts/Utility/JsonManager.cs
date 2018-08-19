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
            assetBundleDirectory = Application.persistentDataPath + "/JsonFolder";
        }



        //제이슨 파일 저장
        public void OnClickSaveJSONBtn()
        {
            Data mydata = new Data();
            string save = JsonUtility.ToJson(mydata, prettyPrint: true);
            Debug.Log(save);
            WriteStringToFile(save, "JsonFile.json");

        }


        //제이슨 파일 로드
        public void OnClickLoadJSONBtn()
        {
            string load = ReadStringFromFile("JsonFile.json");
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
        public Dictionary<int, int> PlayerInventoryData;
        public Dictionary<int, int> PlayerWareHouseData;

        public Data()
        {
            PlayerInfomation = GameDataManager.Instance.PlayerInfomation;
            ATownInformation = GameDataManager.Instance.AtownInformation;
            BTownInformation = GameDataManager.Instance.BtownInformation;
            PlayerInventoryData = GameDataManager.Instance.PlayerGamedata;
            PlayerWareHouseData = GameDataManager.Instance.WareHouseGamedata;

        }
    }
}





