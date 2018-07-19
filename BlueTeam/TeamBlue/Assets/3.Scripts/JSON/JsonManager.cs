
using System.IO;
using UnityEngine;

public class JsonManager : MonoBehaviour
{

    string charname = "hello";
    string birthday = "json";
    bool istired = false;
    bool isseek = true;
    int age = 15;
  

    private void Start()
    {
      
        OnClickSaveJSONBtn();
    }


    //제이슨 파일 저장
    public void OnClickSaveJSONBtn()
    {
        Data mydata = new Data();
        string save = JsonUtility.ToJson(mydata, prettyPrint: true);
        Debug.Log(save);


        writeStringToFile(save, "save.json");


    }

    //제이슨 파일 로드
    public void OnClickLoadJSONBtn()
    {
        string load = readStringFromFile("save.json");
        var loadData = JsonUtility.FromJson<Data>(load);
        charname = loadData.charname;
        birthday = loadData.birthday;
        istired = loadData.istired;
        isseek = loadData.isseek;
        age = loadData.age;

        Debug.Log(load);
        Debug.Log(charname);
    }

    private string readStringFromFile(string path)
    {
        string text = System.IO.File.ReadAllText("Assets/11.JsonFolder" + "/" + path);

        return text;

    }


    private void writeStringToFile(string text, string path)
    {
        string assetBundleDirectory = "Assets/11.JsonFolder";
        // 에셋 번들을 저장할 경로의 폴더가 존재하지 않는다면 생성시킨다.
        if (!Directory.Exists(assetBundleDirectory))
        {
            Directory.CreateDirectory(assetBundleDirectory);
        }


        using (System.IO.StreamWriter file =
             new System.IO.StreamWriter(assetBundleDirectory + "/" + path, true))
        {
            file.WriteLine(text);
        }

    }
}


[SerializeField]
public class Data
{
    public string charname = "hello";
    public string birthday = "json";
    public bool istired = false;
    public bool isseek = true;
    public int age = 15;
    public string[] itemList;

   public Data()
    {
        itemList = new string[4];
        itemList[0] = "철퇴";
        itemList[1] = "대검";
        itemList[2] = "단검";
        itemList[3] = "원거리공격";
    }
}



