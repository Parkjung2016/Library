using System;
using GDP.EventBus;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Parkjung2016
{
    public class SceneManagement : Singleton<SceneManagement>
    {
        private SceneBase _currentScene;
        private CameraManager _cameraManager;


        public SceneBase CurrentScene => _currentScene;

        public CameraManager CameraManager => _cameraManager;


        private void Awake()
        {
            DonDestoroy();

            _cameraManager = new CameraManager();
            SceneBase scene = FindObjectOfType<SceneBase>();
            if (scene != null)
            {
                _cameraManager.Init(scene);
            }

            SceneManager.sceneLoaded += OnLoadScene;
        }

        private void OnLoadScene(Scene arg0, LoadSceneMode arg1)
        {
            ChangeScene(FindObjectOfType<SceneBase>());
        }


        public void ChangeScene(SceneBase sceneBase)
        {
            _currentScene = sceneBase;
            _cameraManager.Init(sceneBase);
        }


        public static void LoadScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }

        public static void LoadScene(int idx)
        {
            SceneManager.LoadScene(idx);
        }
    }
}