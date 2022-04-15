using System.Collections.Generic;
using UnityEngine;

public class NameSetter : MonoBehaviour
{
    [SerializeField] private string _playerName = "You";
    [SerializeField] private List<string> _aINames;

    private List<Car> _cars;

    private void Awake()
    {
        _cars = new List<Car>(GetComponentsInChildren<Car>());
        foreach (var hamster in _cars)
        {
            if (hamster.Type == CarType.Player)
                hamster.SetName(_playerName);
            else
            {
                int randNameIndex = Random.Range(0, _aINames.Count);
                hamster.SetName(_aINames[randNameIndex]);
                _aINames.RemoveAt(randNameIndex);
            }
        }
    }
}
