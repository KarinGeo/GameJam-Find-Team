using Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameWinView : BaseView
{
    int _levelId;

    public override void Awake()
    {
        base.Awake();
    }

    private void OnEnable()
    {
        Find<Button>("Moveable/ReturnBtn").onClick.AddListener(OnReturnBtn);
        Find<Button>("Moveable/NextBtn").onClick.AddListener(OnNextBtn);
    }

    public override void Init(params object[] args)
    {
        GameObject obj = transform.Find("Moveable/Starts").gameObject;
        int stars = (int)args[1];
        
        for (int j = 0; j < stars; j++)
        {
            obj.transform.Find("Star" + (j + 1)).gameObject.SetActive(true);
        }
        _levelId = (int)args[0];
        string level = ((int)args[0] + 1).ToString();
        transform.Find("Moveable/LevelId").GetComponent<Text>().text = "ตฺ" + level + "นุ";

        
    }

    public void OnReturnBtn()
    {
        GameEvents.ResetManager?.Invoke();
        SceneEvents.LoadSceneByName?.Invoke("Start");
    }

    public void OnNextBtn()
    {
        GameEvents.ResetManager?.Invoke();
        SceneEvents.LoadSceneByLevel?.Invoke(_levelId + 1);
    }



}
