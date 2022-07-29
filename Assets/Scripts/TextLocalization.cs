using UnityEngine;
using TMPro;
using Lean.Localization;

[RequireComponent(typeof(TMP_Text))]
public class TextLocalization : MonoBehaviour
{
    [SerializeField] private string _textCodeName;

    private TMP_Text _text;

    private void Start()
    {
        _text = GetComponent<TMP_Text>();

        _text.text = LeanLocalization.GetTranslationText(_textCodeName);
    }
}
