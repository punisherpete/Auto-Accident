using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CarsMenu : MonoBehaviour
{
    public GameObject Buttons;
    public GameObject[] Cars;
    int currentCar = 0;
    void Start()
    {
        Cars[0].SetActive(true);
    }

    public void NextCar()
    {
        Cars[currentCar].SetActive(false);
        currentCar++;
        if (currentCar >= Cars.Length)
            currentCar = 0;
        Cars[currentCar].SetActive(true);
    }
    public void PreviousCar()
    {
        Cars[currentCar].SetActive(false);
        currentCar--;
        if (currentCar < 0)
            currentCar = Cars.Length - 1;
        Cars[currentCar].SetActive(true);
    }
    private void Update()
    {
        if ((Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject()) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && !EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId)))
        {
            Buttons.SetActive(false);
        }
        else if (Input.touchCount == 0 && !Input.GetMouseButton(0))
        {
            Buttons.SetActive(true);
        }
    }
}
