using TMPro;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Mover))]
public class Car : MonoBehaviour
{
    public event UnityAction Finished;

    [SerializeField] private CarType _type;
    [SerializeField] private TMP_Text _nameText;

    private Mover _mover;

    private string _name;
    public string Name => _name;
    public CarType Type => _type;

    private void Awake()
    {
        _mover = GetComponent<Mover>();
    }

    public void SetSpeedLimit(float speed)
    {
        _mover.SetMaxSpeed(speed);
    }

    public void Finish()
    {
        Finished?.Invoke();
    }

    public void SetName(string name)
    {
        _name = name;
        _nameText.text = name;
    }
}
public enum CarType
{
    Player,
    AI
}