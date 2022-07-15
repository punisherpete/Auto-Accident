using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Agava.YandexGames;

public class YandexInitialization : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private IEnumerator Start()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        yield break;
#endif

        yield return YandexGamesSdk.WaitForInitialization();
    }
}
