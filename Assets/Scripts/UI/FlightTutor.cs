using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlightTutor : MonoBehaviour
{
    [SerializeField] private GameObject _flightTutorView;
    [SerializeField] private float _timeToTute;
    [SerializeField] private Data _data;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerInput>())
        {
            if (_data.GetAbilityHandleCarInAir() == false)
            {
                _flightTutorView.gameObject.SetActive(true);
                Time.timeScale = 0.5f;
                _data.SetAbilityToHandleCarInAir();
                _data.Save();
                StartCoroutine(Tuting());
            }
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            CloseTutor();
        }
    }

    private IEnumerator Tuting()
    {
        yield return new WaitForSeconds(_timeToTute);
        CloseTutor();
    }

    public void CloseTutor()
    {
        Time.timeScale = 1f;
        _flightTutorView.gameObject.SetActive(false);
        StopCoroutine(Tuting());
        enabled = false;
    }
}
