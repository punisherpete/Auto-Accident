using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Localization;
using Agava.YandexGames;

public class YandexLocalization : MonoBehaviour
{
    [SerializeField] private LeanLocalization _leanLocalization;

    private void Awake()
    {
        DontDestroyOnLoad(_leanLocalization.gameObject);
    }

    private void Start()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        switch(YandexGamesSdk.Environment.i18n.lang)
        {
            case "ru":
                _leanLocalization.SetCurrentLanguage("Russian");
                break;
            case "en":
                _leanLocalization.SetCurrentLanguage("English");
                break;
            case "tr":
                _leanLocalization.SetCurrentLanguage("Turkish");
                break;
            default:
                _leanLocalization.SetCurrentLanguage("English");
                break;
         }
#endif
    }
}

