using UnityEngine;
using System.IO;
using System;

public static class DebugLog
{
#if UNITY_ANDROID
    static string assetBundleDirectory = Application.persistentDataPath+"/0.DebugLog";
#else
    static string assetBundleDirectory = "Assets/0.DebugLog";
   
#endif
    static string saveText;


    public static void SaveLog(UnityEngine.Object className, string data)
    {

        saveText = DateTime.Now.ToString("yyyy/MM/dd/HH:MM:ss") + " Object Name: " + className.ToString() +
            " Data: " + data.ToString();

        Debug.Log(saveText);
        if (!Directory.Exists(assetBundleDirectory))
        {
            Directory.CreateDirectory(assetBundleDirectory);
        }


        FileStream fs = new FileStream(assetBundleDirectory + "/DebugLog.txt", FileMode.Append, FileAccess.Write);

        using (StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.UTF8))
        {
            sw.WriteLine(saveText);
        }

    }

}


