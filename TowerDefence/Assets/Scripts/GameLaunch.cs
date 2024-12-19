using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

using Framework;

public class GameLaunch : MonoBehaviour
{
    private void Awake()
    {
        this.InitFramwork();

        this.InitOthers();

        this.CheckHotUpdate();

        this.InitGameLogic();
    }


    private void Start()
    {
        
    }

    private void CheckHotUpdate()
    { 
        
    }

    private void InitFramwork()
    {
        this.gameObject.AddComponent<ResourceManager>();
        this.gameObject.AddComponent<ViewManager>();
        this.gameObject.AddComponent<ControllerManager>();
        this.gameObject.AddComponent<SceneLoadManager>();
        this.gameObject.AddComponent<ConfigManager>();
        this.gameObject.AddComponent<UserDataManager>();
    }

    private void InitGameLogic()
    {
        this.gameObject.AddComponent<GameApp>();
        this.gameObject.AddComponent<StateManager>();
        this.gameObject.AddComponent<GameplayManager>();
        this.gameObject.AddComponent<InputManager>();
        GameEvents.StartGame?.Invoke();
    }

    //��ResourceManager��Ч֮��ִ��, �����ResourceManager
    private void InitOthers()
    {
        //���������ʽ
        Texture2D mouseText = ResourceManager.Instance.GetAssetCache<Texture2D>("Other/Mouse.png");

        Cursor.SetCursor(mouseText, Vector2.zero, CursorMode.Auto);
    }


}
