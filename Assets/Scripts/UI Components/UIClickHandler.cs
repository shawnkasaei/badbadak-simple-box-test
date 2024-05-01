using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class UIClickHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public UnityEvent OnClickStarted;
    public UnityEvent OnClickDone;
    
    [HideInInspector] public bool isPressed;
    
    public virtual void OnPointerDown(PointerEventData eventData)
    {
        isPressed = true;
        OnClickStarted.Invoke();
    }

    public virtual void OnPointerUp(PointerEventData eventData)
    {
        isPressed = false;
        OnClickDone.Invoke();
    }
}
