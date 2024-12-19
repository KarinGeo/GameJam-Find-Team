using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ConfigData
{
    private string _fileName;

    private List<string> _titles;

    private Dictionary<int, Dictionary<string, string>> _datas;

    private Dictionary<string, List<string>> _dataList;

    public string FileName => _fileName;

    public ConfigData(string fileName)
    {
        this._fileName = fileName;
        this._datas = new Dictionary<int, Dictionary<string, string>>();
        this._dataList = new Dictionary<string, List<string>>();
        this._titles = new List<string>();
    }

    public TextAsset LoadConfigFile()
    {
        string path = Path.Combine(Application.streamingAssetsPath, _fileName + ".csv");
        if (!File.Exists(Path.Combine(Application.streamingAssetsPath, "UserLevelData.csv")))
        {
            path = Path.Combine(Application.streamingAssetsPath, _fileName + ".txt");
        }

        return new TextAsset(File.ReadAllText(path));
    }

    public TextAsset LoadUserFile()
    {
        string path = Path.Combine(Application.persistentDataPath, _fileName + ".csv");
        if (!File.Exists(path))
        {
            path = Path.Combine(Application.persistentDataPath, _fileName + ".txt");
        }
        return new TextAsset(File.ReadAllText(path));
    }

    public void Load(string txt)
    {
        string[] dataArr = txt.Split("\n"); //换行
        string[] titleArr = dataArr[0].Trim().Split(','); //逗号切割 //获得第一行数据 作为每行数据中字典的key

        for (int i = 0; i < titleArr.Length; i++)
        {
            _titles.Add(titleArr[i]);
            _dataList[titleArr[i]] = new List<string>();
        }

        //内容从第三行开始读取 （下标从2开始）
        for (int i = 2; i < dataArr.Length - 1; i++)
        {
            string[] tempArr = dataArr[i].Trim().Split(",");
            Dictionary<string, string> tempData = new Dictionary<string, string>();

            for (int j = 0; j < tempArr.Length; j++)
            {
                tempData.Add(titleArr[j], tempArr[j]);
                _dataList[titleArr[j]].Add(tempArr[j]);
            }
            _datas.Add(int.Parse(tempData["Id"]), tempData);
        }
    }

    public Dictionary<string, string> GetDataById(int id)
    {
        if (_datas.ContainsKey(id))
        {
            return _datas[id];
        }
        return null;
    }

    public List<string> GetDataByTitle(string title)
    {
        if (_dataList.ContainsKey(title))
        {
            return _dataList[title];
        }
        return null;
    }

    public List<string> GetTitle()
    {
        return _titles;
    }

    public Dictionary<int, Dictionary<string, string>> GetLines()
    {
        return _datas;
    }
}

