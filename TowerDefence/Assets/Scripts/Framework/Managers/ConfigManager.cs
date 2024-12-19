using Framework;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Framework
{
    public class ConfigManager : UnitySingleton<ConfigManager>
    {
        private Dictionary<string, ConfigData> _loadList; //��Ҫ��ȡ�����ñ�
        private Dictionary<string, ConfigData> _configs; //�Ѿ�������ɵ����ñ�

        public override void Awake()
        {
            _loadList = new Dictionary<string, ConfigData>();
            _configs = new Dictionary<string, ConfigData>();
        }

        public void Register(string file, ConfigData config)
        {
            _loadList[file] = config;
        }

        public void LoadAllConfigs()
        {
            foreach (var item in _loadList)
            {
                TextAsset textAsset = item.Value.LoadConfigFile();
                item.Value.Load(textAsset.text);
                _configs.Add(item.Value.FileName, item.Value);
            }

            _loadList.Clear();
        }

        public ConfigData GetConfigData(string file)
        {
            if (_configs.ContainsKey(file))
            {
                return _configs[file];
            }
            else
            {
                return null;
            }
        }
    }

}
