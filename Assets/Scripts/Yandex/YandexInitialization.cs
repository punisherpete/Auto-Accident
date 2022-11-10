using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Agava.YandexGames;
using System;

public class YandexInitialization : MonoBehaviour
{
    [SerializeField] private ScreensaverSceneManager _screensaverSceneManager;

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
#if !UNITY_WEBGL || UNITY_EDITOR
        ChangeScene();
        yield break;
#endif

#if VK_GAMES
        yield return Agava.VKGames.VKGamesSdk.Initialize(ChangeScene);
#endif

#if YANDEX_GAMES
        yield return YandexGamesSdk.Initialize(ChangeScene);
#endif

        yield return YandexGamesSdk.WaitForInitialization();
    }

    private void ChangeScene()
    {
        _screensaverSceneManager.StarGame();
    }
}
