using Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Framework
{
    public class UserDataManager : UnitySingleton<UserDataManager>
    {
        private Dictionary<string, ConfigData> _loadList; //需要读取的配置表
        private Dictionary<string, ConfigData> _userDatas; //已经加载完成的配置表

        public override void Awake()
        {
            _loadList = new Dictionary<string, ConfigData>();
            _userDatas = new Dictionary<string, ConfigData>();
        }

        public void Register(string file, ConfigData data)
        {
            _loadList[file] = data;
        }

        public void ReRegister(string file, ConfigData data)
        {
            _loadList.Remove(file);
            _loadList[file] = data;
        }

        public void LoadAllUserDatas()
        {
            foreach (var item in _loadList)
            {
                TextAsset textAsset = item.Value.LoadUserFile();
                item.Value.Load(textAsset.text);
                _userDatas.Add(item.Value.FileName, item.Value);
            }

            _loadList.Clear();
        }

        public ConfigData GetUserData(string file)
        {
            if (_userDatas.ContainsKey(file))
            {
                return _userDatas[file];
            }
            else
            {
                return null;
            }
        }
    }

}
