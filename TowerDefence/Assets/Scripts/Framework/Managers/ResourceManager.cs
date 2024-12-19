using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

namespace Framework
{
    public class ResourceManager : UnitySingleton<ResourceManager>
    {
        public override void Awake()
        {
            base.Awake();
        }

        public T GetAssetCache<T>(string name) where T : UnityEngine.Object
        {
#if UNITY_EDITOR
            {
                string path = "Assets/AssetsPackage/" + name;
                UnityEngine.Object target = UnityEditor.AssetDatabase.LoadAssetAtPath<T>(path);

                return target as T;
            }
#endif
        }

        public T GetDataCache<T>(string name) where T : UnityEngine.Object
        {
#if UNITY_EDITOR
            {
                string path = "Assets/Data/" + name;

                UnityEngine.Object target = UnityEditor.AssetDatabase.LoadAssetAtPath<T>(path);

                return target as T;
            }
#endif
        }
    }
}


