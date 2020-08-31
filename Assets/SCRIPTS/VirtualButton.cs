using UnityEngine;
using UnityEngine.EventSystems;

public class VirtualButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public string button = "";
    public string player = "";

    bool pressed = false;
    bool release = false;

    public void OnPointerDown(PointerEventData eventData)
    {
        InputManager.Instance.SetButtonState(button + player, InputManager.ButtonStates.Down);
        pressed = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        InputManager.Instance.SetButtonState(button + player, InputManager.ButtonStates.Up);
        pressed = false;
        release = true;
    }

    private void LateUpdate()
    {
        if (pressed)
        {
            InputManager.Instance.SetButtonState(button + player, InputManager.ButtonStates.Pressed);
            pressed = false;
        }
        if (release)
        {
            InputManager.Instance.SetButtonState(button + player, InputManager.ButtonStates.NoPressed);
            release = false;
        }
    }
}
