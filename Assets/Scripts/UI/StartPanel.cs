using UnityEngine;
using UnityEngine.Events;

public class StartPanel : MonoBehaviour
{
    public UnityEvent ActivateAfterDown;
    
    [SerializeField] private MainMenu _mainMenu;

    private void OnEnable()
     {
         _mainMenu.ClickButton += OnPointerDown;
     }
    
     private void OnDisable()
     {
         _mainMenu.ClickButton -= OnPointerDown;
     }

    private void OnPointerDown()
    {
        ActivateAfterDown?.Invoke();
        gameObject.SetActive(false);
    }
}
