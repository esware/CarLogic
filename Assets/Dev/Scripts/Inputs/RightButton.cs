using Managers;
using UnityEngine;
using UnityEngine.EventSystems;

namespace VHS
{
    public class RightButton:MonoBehaviour,IPointerDownHandler,IPointerUpHandler
    {
        public void OnPointerDown(PointerEventData eventData)
        {
            GameEvents.InputEvents.RightButtonClicked?.Invoke();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            GameEvents.InputEvents.RightButtonReleased?.Invoke();
        }
    }
}