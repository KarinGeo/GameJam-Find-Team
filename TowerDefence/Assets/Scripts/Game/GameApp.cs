using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.Rendering;

namespace Framework
{
    public class GameApp : UnitySingleton<GameApp>
    {
        public override void Awake()
        {
            RegisterControllers();
            RegisterConfigs();
            RegisterUserDatas();
            LoadDatas();
        }

        public void OnEnable()
        {
            GameEvents.StartGame += GameEvent_StartGame;
        }

        public void OnDisable()
        {
            GameEvents.StartGame -= GameEvent_StartGame;
        }

        public void GameEvent_StartGame()
        {
            EnterStartScene();
            OpenStartView();
        }

        public void EnterStartScene()
        {
            SceneEvents.LoadSceneByName?.Invoke("Start");
        }

        public void OpenStartView()
        {
            UIEvents.StartViewOpen?.Invoke();
        }


        private void RegisterControllers()
        {
            ControllerManager.Instance.Register(ControllerType.UI, new UIController());
            ControllerManager.Instance.Register(ControllerType.Build, new BuildController());
            ControllerManager.Instance.Register(ControllerType.Gameplay, new GameplayController());
        }

        private void RegisterUserDatas()
        {
            string userDataPath = Path.Combine(Application.persistentDataPath, "UserLevelData.csv");
            if (!File.Exists(userDataPath))
            {
                using (StreamWriter writer = new StreamWriter(userDataPath, false, new UTF8Encoding(true)))
                {
                    writer.WriteLine("Id,Lock,Star");
                    writer.WriteLine("±àºÅ,ÊÇ·ñ½âËø,ÐÇÊý");

                    writer.WriteLine("1001,false,0");
                    for (int i = 1; i < 12; i++)
                    {
                        int id = 1000 + i + 1;
                        writer.WriteLine(id + ",true,0");
                    }
                }
            }

            UserDataManager.Instance.Register("UserLevelData", new ConfigData("UserLevelData"));
        }

        private void RegisterConfigs()
        {
            ConfigManager.Instance.Register("EnemyData", new ConfigData("EnemyData"));
            ConfigManager.Instance.Register("TowerData", new ConfigData("TowerData"));
        }

        private void LoadDatas()
        {
            ConfigManager.Instance.LoadAllConfigs();
            UserDataManager.Instance.LoadAllUserDatas();
        }
    }
}