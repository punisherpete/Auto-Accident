using System;
using System.Collections;
using UnityEngine;
using Agava.YandexGames;

public class YandexInitialization : MonoBehaviour
{
    [SerializeField] private ScreensaverSceneManager _manager;

    private event Action Initialized;
    
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        Initialized += ChangeScene;
    }

    private void OnDisable()
    {
        Initialized -= ChangeScene;
    }
    private IEnumerator Start()
    {
#if YANDEX_GAMES
        ChangeScene();
        yield return YandexGamesSdk.WaitForInitialization();
#endif
        
#if !UNITY_WEBGL || UNITY_EDITOR
        yield break;
#endif
    }

    private void ChangeScene()
    {
        _manager.StartGame();
    }
}
