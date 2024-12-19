using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class UICreator : EditorWindow
{
    private static string filePath = "Scripts/Game/UIControllers/";

    [MenuItem("MyWindow/UICreator")]
    public static void CreateUI()
    {
        UICreator win = EditorWindow.GetWindow<UICreator>();
        win.titleContent.text = "UICreator";
        win.Show();
    }

    public void OnGUI()
    {
        GUILayout.Label(StateManager.Instance.CurrentStateInterface.Name);

        if (Selection.activeGameObject != null)
        {
            GUILayout.Label(Selection.activeGameObject.name);
            GUILayout.Label(filePath + Selection.activeGameObject.name + "Controller");
        }

        //GUILayout.Label("ѡ��һ��UI��ͼ");
        //if (Selection.activeGameObject != null)
        //{
        //    GUILayout.Label(Selection.activeGameObject.name);
        //    GUILayout.Label(filePath + Selection.activeGameObject.name + "Controller");
        //}
        //else 
        //{
        //    GUILayout.Label("û��ѡ�е�UI�ڵ㣬�޷�����");
        //}

        //if (GUILayout.Button("����UI�����ļ�"))
        //{
        //    if (Selection.activeGameObject != null)
        //    { 
        //        string className = Selection.activeGameObject.name + "Controller";
        //        UICreatorUtil.GenerateUIControllerFile(filePath, className);
        //    }
        //}
    }

    public void OnSelectionChange()
    {
        this.Repaint();
    }
}
