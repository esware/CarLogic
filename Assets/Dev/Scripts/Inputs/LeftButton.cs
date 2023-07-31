using Managers;
using UnityEngine;
using UnityEngine.EventSystems;

namespace VHS
{
    public class LeftButton:MonoBehaviour,IPointerDownHandler,IPointerUpHandler
    {
        public void OnPointerDown(PointerEventData eventData)
        {
            GameEvents.InputEvents.LeftButtonClicked?.Invoke();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            GameEvents.InputEvents.LeftButtonReleased?.Invoke();
        }
    }
}