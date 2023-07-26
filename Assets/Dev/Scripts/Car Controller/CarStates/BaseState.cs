using UnityEngine;

namespace Dev.Scripts.Car_Controller.CarStates
{
    public abstract class BaseState
    {
        private static CarController _controller;

        public static CarController GetController => _controller;
        
        public BaseState(CarController controller)
        {
            _controller = controller;
        }


        public abstract void Update();
        public abstract void FixedUpdate();
        public virtual void LateUpdate(){}
    }
}