using Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
namespace Framework
{
    public class SceneLoadManager : UnitySingleton<StateManager>
    {
        const string START = "Start";
        const string LEVEL = "Level";

        float fadeTime = 1f;
        Color color;

        private Image _transitionImage;

        public override void Awake()
        {
            _transitionImage = transform.Find("Canvas/Transition Image").GetComponent<Image>();
        }

        private void OnEnable()
        {
            SceneEvents.LoadSceneByName += SceneEvents_LoadSceneByName;
            SceneEvents.LoadSceneByLevel += SceneEvents_LoadSceneByLevel;
        }

        private void OnDisable()
        {
            SceneEvents.LoadSceneByName -= SceneEvents_LoadSceneByName;
            SceneEvents.LoadSceneByLevel -= SceneEvents_LoadSceneByLevel;
        }

        private void SceneEvents_LoadSceneByName(string sceneName)
        {
            StartCoroutine(LoadScene(sceneName));
        }

        private void SceneEvents_LoadSceneByLevel(int levelId)
        {
            
            StartCoroutine(LoadLevelScene(LEVEL));

        }

        void Load(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }

        IEnumerator LoadScene(string sceneName)
        {
            AsyncOperation loadingOperation = SceneManager.LoadSceneAsync(sceneName);
            loadingOperation.allowSceneActivation = false;

            _transitionImage.gameObject.SetActive(true);
            while (color.a < 1f)
            {
                color.a = Mathf.Clamp01(color.a + Time.unscaledDeltaTime / fadeTime);
                _transitionImage.color = color;

                yield return null;
            }
            UIEvents.AllOpenViewsClose?.Invoke();
            loadingOperation.allowSceneActivation = true;
            UIEvents.StartViewOpen?.Invoke();
            while (color.a > 0f)
            {
                color.a = Mathf.Clamp01(color.a - Time.unscaledDeltaTime / fadeTime);
                _transitionImage.color = color;

                yield return null;
            }

            _transitionImage.gameObject.SetActive(false);
        }

        IEnumerator LoadLevelScene(string sceneName)
        {
            Debug.Log("÷¥––º”‘ÿπÿø®");
            AsyncOperation loadingOperation = SceneManager.LoadSceneAsync(sceneName);
            loadingOperation.allowSceneActivation = false;

            _transitionImage.gameObject.SetActive(true);

            while (color.a < 1f)
            {
                color.a = Mathf.Clamp01(color.a + Time.unscaledDeltaTime / fadeTime);
                _transitionImage.color = color;

                yield return null;
            }

            UIEvents.AllOpenViewsClose?.Invoke();
            loadingOperation.allowSceneActivation = true;
            loadingOperation.completed += LoadSceneInfo;
            UIEvents.GameplayViewOpen?.Invoke();
            while (color.a > 0f)
            {
                color.a = Mathf.Clamp01(color.a - Time.unscaledDeltaTime / fadeTime);
                _transitionImage.color = color;

                yield return null;
            }

            

            _transitionImage.gameObject.SetActive(false);
        }

        public void LoadSceneInfo(AsyncOperation operation)
        {
            GameEvents.LoadSceneInfo?.Invoke();
        }

    }
}