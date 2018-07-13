using UnityEngine;
using System.IO;
using System;

public static class DebugClass
{
    static string savePath = "Assets/0.DebugLog/DebugLog.txt";
    static string saveText;


    public static void SaveAndPrintDebugLog(UnityEngine.Object className, string data)
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


