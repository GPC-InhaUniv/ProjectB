using UnityEngine;
using System.IO;
using System;

public static class DebugLog
{
#if UNITY_ANDROID
    static string savePath =Application.persistentDataPath+"/0.DebugLog/DebugLog.txt";
#else
    static string savePath = "Assets/0.DebugLog/DebugLog.txt";
   
#endif
    static string saveText;


    public static void SaveLog(UnityEngine.Object className, string data)
    {

        saveText = DateTime.Now.ToString("yyyy/MM/dd/HH:MM:ss") + " Object Name: " + className.ToString() +
            " Data: " + data.ToString();

        Debug.Log(saveText);
        FileStream fs = new FileStream(savePath, FileMode.Append, FileAccess.Write);

        using (StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.UTF8))
        {
            sw.WriteLine(saveText);
        }

    }

}


