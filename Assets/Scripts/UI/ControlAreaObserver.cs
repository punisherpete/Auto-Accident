using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ControlAreaObserver : MonoBehaviour, IPointerClickHandler,IPointerDownHandler
{
    public UnityEvent ActivateAfterDowned;
    public UnityEvent ActivateAfterClicked;

    public void OnPointerDown(PointerEventData eventData)
    {
        ActivateAfterDowned?.Invoke();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        ActivateAfterClicked?.Invoke();
    }
}


