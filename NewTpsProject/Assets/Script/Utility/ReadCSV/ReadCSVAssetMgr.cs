using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using System.Xml;

namespace Assets.Script.Utility
{

    public enum CsvNameType
    {
        WeaponsData,
        Max,
    }
    public class ReadCSVAssetMgr : MonoBehaviour
    {

        public static CSVAnalysis GetXmlData(CsvNameType name)
        {
            switch (name)
            {
                case CsvNameType.WeaponsData:
                    return new WeaponsData();
                case CsvNameType.Max:
                    return null;
                default:
                    return null;
            }
        }

        //文件名返回路径
        public static string FileCSV(string _fileName)
        {
            string path = "";
#if UNITY_STANDALONE
        path = Application.dataPath + "/StreamingAssets/Config/CSV/" + _fileName + ".csv";
#elif UNITY_EDITOR
            path = Application.dataPath + "/StreamingAssets/Config/CSV/" + _fileName + ".csv";
#elif UNITY_IPHONE
    path = Application.dataPath + "/Raw/Config/Csv/" + _fileName + ".csv";
#elif UNITY_ANDROID
    path = "jar:file://" + Application.dataPath + "!/assets/Config/CSV/"+ _fileName +".csv";
#endif
            Debug.Log(path);
            return path;
        }

        //解析文件
        public static List<string[]> ReadCSV(string _fileName)
        {
            //  Debug.Log("解析");
            List<string[]> data = new List<string[]>();
            string path = FileCSV(_fileName);
            if (File.Exists(path))
            {
                try
                {
                    StreamReader srReadFile = new StreamReader(path, Encoding.UTF8);
                    while (!srReadFile.EndOfStream)
                    {
                        //检索出行
                        UTF8Encoding utf8 = new UTF8Encoding();
                        string value = utf8.GetString(utf8.GetBytes(srReadFile.ReadLine()));
                        data.Add(GetItemList(value));
                        // Debug.Log(value);
                    }
                    // 关闭读取流文件
                    srReadFile.Close();
                }
                catch (Exception e)
                {
                    Debug.LogError(e.Message + "请检查本地文件是否打开" + _fileName);
                }

            }
            else
            {
                WWW www = new WWW(path);
                while (!www.isDone) { }

                MemoryStream stream = new MemoryStream(www.bytes);
                StreamReader reader = new StreamReader(stream);
                while (!reader.EndOfStream)
                {
                    //检索出行
                    UTF8Encoding utf8 = new UTF8Encoding();
                    string value = utf8.GetString(utf8.GetBytes(reader.ReadLine()));
                    data.Add(GetItemList(value));
                    Debug.Log(value);
                }
                stream.Close();
                reader.Close();

                Debug.Log(www.text);
            }
            return data;
        }

        private static string[] GetItemList(string line)
        {
            string[] values = line.Split(',');
            List<string> valueList = new List<string>(values.Length);
            int startIndex = values.Length, endIndex = 0, needChangeCount = 0;
            for (int i = 0; i < values.Length; i++)
            {
                if (values[i].StartsWith("\"") && values[i].EndsWith("\"") == false)
                {
                    startIndex = i;
                    endIndex = values.Length;
                }

                if (i > startIndex && i < endIndex)
                {

                    values[startIndex] += "," + values[i];
                    if (values[i].EndsWith("\""))
                    {
                        endIndex = i;
                        int listIndex = valueList.Count <= startIndex ? startIndex - needChangeCount : startIndex;
                        valueList[listIndex] = values[startIndex];
                    }
                    needChangeCount++;
                    values[i] = string.Empty;
                }

                if (String.IsNullOrEmpty(values[i]) == false)
                {
                    valueList.Add(values[i]);
                }
            }
            return valueList.ToArray();
        }
    }
}
