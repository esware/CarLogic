using System;
using Managers;
using UnityEngine;

namespace VHS
{
    public class InputHandler : MonoBehaviour
    {
        private static InputHandler _instance;
        public static InputHandler Instance => _instance;

        private int _direction;
        public int Direction => _direction;

        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject); 
            }
            else
            {
                _instance = this; 
                DontDestroyOnLoad(gameObject);
                SignUpEvents();
            }
        }

        private void SignUpEvents()
        {
            GameEvents.InputEvents.LeftButtonClicked += TurnLeft;
            GameEvents.InputEvents.RightButtonClicked += TurnRight;
            GameEvents.InputEvents.LeftButtonReleased += TurnForward;
            GameEvents.InputEvents.RightButtonReleased += TurnForward;
        }

        private void TurnLeft() => _direction = -1;

        private void TurnRight() => _direction = 1;

        private void TurnForward() => _direction = 0;
    }
}
