using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class VirtualJoystic : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    [SerializeField] RectTransform stick = null;
    [SerializeField] Image Background = null;
    
    float limit = 200f;
    public string player = "";

    public void OnPointerDown(PointerEventData eventData)
    {
        stick.anchoredPosition = ConvertToLocal(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 pos = ConvertToLocal(eventData);
        if (pos.magnitude > limit)
            pos = pos.normalized * limit;
        stick.anchoredPosition = pos;

        float x = pos.x / limit;
        float y = pos.y / limit;

        SetHorizontal(x);
        SetVertical(y);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        stick.anchoredPosition = Vector2.zero;
        SetHorizontal(0);
        SetVertical(0);
    }

    private void OnDisable()
    {
        SetHorizontal(0);
        SetVertical(0);
    }

    void SetHorizontal(float val)
    {
        InputManager.Instance.SetAxis("Horizontal" + player, val);
    }

    void SetVertical(float val)
    {
        InputManager.Instance.SetAxis("Vertical" + player, val);
    }

    Vector2 ConvertToLocal(PointerEventData eventData)
    {
        Vector2 newPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            transform as RectTransform,
            eventData.position,
            eventData.enterEventCamera,
            out newPos);
        return newPos;
    }
}
