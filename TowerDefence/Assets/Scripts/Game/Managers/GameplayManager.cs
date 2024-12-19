using Framework;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEngine.GraphicsBuffer;

public partial class GameplayManager : UnitySingleton<GameplayManager>
{
    private int _levelId = 0;
    private int _health = 3;
    private int _gold = 1000;

    private WaveManager _waveManager;

    public int LevelId => _levelId;
    public int Health => _health;
    public int Gold => _gold;

    public bool _isLoaded; //�Ƿ��Ѿ����ص�ǰ�ؿ���������

    private List<List<Vector3>> _paths;

    private AllLevelSO _levelsData;

    public override void Awake()
    {
        this._waveManager = new WaveManager();
        this._waveManager.SetCoroutineRunner(this);
        this._levelsData = ResourceManager.Instance.GetDataCache<AllLevelSO>("LevelData/AllLevels.asset");

        this._isLoaded = false;

        _paths = new List<List<Vector3>>();
    }

    public override void ResetSelf()
    {
        base.ResetSelf();

        _levelId = 0;
        _health = 3;
        _gold = 1000;

        this._waveManager.ResetSelf();
        this._waveManager.SetCoroutineRunner(this);

        this._isLoaded = false;

        _paths = new List<List<Vector3>>();
        this._levelsData = ResourceManager.Instance.GetDataCache<AllLevelSO>("LevelData/AllLevels.asset");
    }


    public void Update()
    {
        if (_health <= 0)
        {
            Debug.Log("��Ϸʧ��");
        }
    }


    public void OnEnable()
    {
        SceneEvents.LoadSceneByLevel += SceneEvents_LoadSceneByLevel;
        GameEvents.LoadSceneInfo += GameEvent_LoadSceneInfo;
        GameEvents.HealthDown += GameEvent_HealthDown;
        GameEvents.GoldDown += GameEvent_GoldDown;
        GameEvents.GoldUp += GameEvent_GoldUp;
        GameEvents.GameWin += GameEvent_GameWin;
        GameEvents.ResetManager += GameEvent_ResetManager;
    }

    public void OnDisable()
    {
        SceneEvents.LoadSceneByLevel -= SceneEvents_LoadSceneByLevel;
        GameEvents.LoadSceneInfo -= GameEvent_LoadSceneInfo;
        GameEvents.HealthDown -= GameEvent_HealthDown;
        GameEvents.GoldDown -= GameEvent_GoldDown;
        GameEvents.GoldUp -= GameEvent_GoldUp;
        GameEvents.GameWin -= GameEvent_GameWin;
        GameEvents.ResetManager -= GameEvent_ResetManager;
    }

    public void GameEvent_HealthDown()
    {
        _health -= 1;
        UIEvents.GameplayViewRefresh?.Invoke();
    }

    public void GameEvent_ResetManager()
    {
        ResetSelf();
    }

    public void GameEvent_GoldDown(int cost)
    {
        _gold -= cost;
        UIEvents.GameplayViewRefresh?.Invoke();
    }

    public void GameEvent_GoldUp(int gain)
    {
        _gold += gain;
        UIEvents.GameplayViewRefresh?.Invoke();
    }


    public void GameEvent_GameWin()
    {
        string filePath = Path.Combine(Application.persistentDataPath, "UserLevelData.csv");
        if (File.Exists(filePath))
        {
            List<string> lines = new List<string>(File.ReadAllLines(filePath, Encoding.UTF8));
            for (int i = 2; i < lines.Count; i++) // �ӵ� 2 �п�ʼ������ͷ
            {
                string[] columns = lines[i].Split(','); // �����ŷָ�

                int currentLevelId = _levelId + 1 + 1000;
                int nextLevelId = currentLevelId + 1;

                if (columns.Length > 0 && int.TryParse(columns[0], out int currentId))
                {
                    if (currentId == currentLevelId)
                    {
                        // ����ָ����
                        
                        if (int.TryParse(columns[2], out int star) && star < _health)
                        {
                            Debug.Log("��������Ϊ" + _health.ToString());
                            columns[2] = _health.ToString(); // �޸� Star ��
                            lines[i] = string.Join(",", columns); // ���������
                        }
                    }
                    if (currentId == nextLevelId)
                    {
                        // ����ָ����
                        Debug.Log("����" + nextLevelId.ToString() + "Ϊ");
                        columns[1] = "FALSE"; // �޸� Star ��
                        lines[i] = string.Join(",", columns); // ���������
                    }

                }
            }
            File.WriteAllLines(filePath, lines, Encoding.UTF8);
            UserDataManager.Instance.ReRegister("UserLevelData", new ConfigData("UserLevelData"));
        }
        UIEvents.GameWinViewOpen?.Invoke();
    }

    public void GameEvent_LoadSceneInfo()
    {
        if (!_isLoaded)
        {
            GameObject allPath = GameObject.Find("Map/Path");

            foreach (Transform path in allPath.transform)
            {
                List<Vector3> tempPath = new List<Vector3>();
                foreach (Transform point in path.transform)
                {
                    tempPath.Add(point.position);
                }
                _paths.Add(tempPath);
            }

            GameObject TowerBase = GameObject.Find("Map/TowerBase");
            foreach (Transform towerBase in TowerBase.transform)
            {
                towerBase.AddComponent<TowerBase>();
            }

            LevelSO levelData = _levelsData.Levels[_levelId];

            _waveManager.Set(_paths, levelData);
            //BuildManager.Instance.Set();
            _isLoaded = true;
        }


    }

    public void SceneEvents_LoadSceneByLevel(int levelId)
    { 
        this._levelId = levelId;
    }



   
}
