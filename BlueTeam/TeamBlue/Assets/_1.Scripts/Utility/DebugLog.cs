using UnityEngine;
using System.IO;
using System;

namespace ProjectB.Utility
{
    public static class DebugLog
    {
#if UNITY_ANDROID
        static string assetBundleDirectory = Application.persistentDataPath + "/DebugLog";
#else
    static string assetBundleDirectory = "Assets/0.DebugLog";
   
#endif
        static string saveText;


        public static void SaveLog(UnityEngine.Object className, string data)
        {
            string currentTime = DateTime.Now.ToString("yyyy/MM/dd/HH:MM:ss");
            string currentTimeForSaving = DateTime.Now.ToString("yyyy_MM_dd");
            saveText = currentTime + " Object Name: " + className.ToString() +
                " Data: " + data.ToString();

            Debug.Log(saveText);

            if (!Directory.Exists(assetBundleDirectory))
            {
                Directory.CreateDirectory(assetBundleDirectory);
            }


            FileStream fs = new FileStream(assetBundleDirectory + "/" + currentTimeForSaving + "_" + "DebugLog.txt", FileMode.Append, FileAccess.Write);

            using (StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.UTF8))
            {
                sw.WriteLine(saveText);
            }

        }

    }

}
