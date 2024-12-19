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
        string[] dataArr = txt.Split("\n"); //����
        string[] titleArr = dataArr[0].Trim().Split(','); //�����и� //��õ�һ������ ��Ϊÿ���������ֵ��key

        for (int i = 0; i < titleArr.Length; i++)
        {
            _titles.Add(titleArr[i]);
            _dataList[titleArr[i]] = new List<string>();
        }

        //���ݴӵ����п�ʼ��ȡ ���±��2��ʼ��
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

