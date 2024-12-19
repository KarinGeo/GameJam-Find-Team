using Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SelectLevelView : BaseView
{
    private List<Button> _levelButtons;

    public override void Awake()
    {
        base.Awake();
        _levelButtons = new List<Button>();
    }

    private void OnEnable()
    {
        Find<Button>("Background/ReturnBtn").onClick.AddListener(() => UIEvents.ViewReturn?.Invoke());

        SceneEvents.LoadSceneByLevel += (id) => DisableButtonClick();
    }

    private void OnDisable()
    {
        SceneEvents.LoadSceneByLevel -= (id) => DisableButtonClick(); 
    }

    public override void Init(params object[] args)
    {
        base.Init(args);

        foreach (Transform child in Find("Grid").transform)
        {
            if (child.gameObject.activeSelf)
            {
                Destroy(child.gameObject);
            }
        }

        GameObject levelPrefabObj = Find("Grid/Level");
        Transform gridTf = Find("Grid").transform;

        Debug.Log(Application.persistentDataPath);

        ConfigData userLevelData = UserDataManager.Instance.GetUserData("UserLevelData");

        List<string> levels = userLevelData.GetDataByTitle("Id");

        for (int i = 0; i < levels.Count; i++)
        {
            bool isLock = bool.Parse(userLevelData.GetDataById(int.Parse(levels[i]))["Lock"]);

            GameObject obj = Object.Instantiate(levelPrefabObj, gridTf);
            if (isLock)
            {
                obj.transform.Find("Unlock").gameObject.SetActive(false);
                obj.transform.Find("Lock").gameObject.SetActive(true);
            }
            else
            {
                int stars = int.Parse(userLevelData.GetDataById(int.Parse(levels[i]))["Star"]);
                obj.transform.Find("Unlock").gameObject.SetActive(true);
                obj.transform.Find("Unlock/txt").GetComponent<Text>().text = (int.Parse(levels[i]) - 1000).ToString();
                obj.transform.Find("Lock").gameObject.SetActive(false);
                for (int j = 0; j < stars; j++)
                {
                    obj.transform.Find("Unlock/Star" + (j + 1)).gameObject.SetActive(true);
                }

                string levelId = levels[i];
                Button btn = obj.GetComponent<Button>();
                int currentIndex = i;
                //btn.onClick.AddListener(() => SceneEvents.LoadSceneByLevel?.Invoke(int.Parse(obj.transform.Find("Unlock/txt").GetComponent<Text>().text)));

                
                btn.onClick.AddListener(() => SceneEvents.LoadSceneByLevel?.Invoke(currentIndex));
                _levelButtons.Add(btn);
            }

            obj.SetActive(true);
        }


    }


    //使按钮失效，避免连点
    public void DisableButtonClick()
    {
        if (_levelButtons.Count != 0)
        {
            foreach (Button btn in _levelButtons)
            {
                btn.interactable = false;
            }
        }
    }

    private void onClickLevel(string id)
    {

        //GameApp.ViewManager.Close(ViewId);
        //GameApp.ViewManager.Open(ViewType.SelectPawnView, id);
    }


}
