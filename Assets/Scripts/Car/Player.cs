using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerType _type;
    [SerializeField] private TMP_Text _nameText;

    private string _name;
    public string Name => _name;
    public PlayerType Type => _type;
}
public enum PlayerType
{
    Player,
    AI
}