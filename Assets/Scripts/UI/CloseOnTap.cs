using UnityEngine.EventSystems;
using UnityEngine;
using System.Collections.Generic;

public class CloseOnTap : MonoBehaviour
{
    [SerializeField] private MainMenu _mainMenu;

    private void OnEnable()
    {
        EventSystem.current.SetSelectedGameObject(gameObject);
    }

    private void Update()
    {
        PointerEventData pointer = new PointerEventData(EventSystem.current);

        if (Input.GetMouseButtonDown(0))
        {
            pointer.position = Input.mousePosition;

            List<RaycastResult> raycastResults = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointer, raycastResults);

            if (raycastResults.Count == 0)
            {
                CloseSelf();
            }
        }
    }

    public void CloseSelf()
    {
        gameObject.SetActive(false);
        _mainMenu.EnableGameElements();
    }
}