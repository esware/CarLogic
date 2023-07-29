using Managers;
using UnityEngine;

namespace Dev.Scripts.Car_Controller.CarStates
{
    public class CarWinState:BaseState
    {
        public CarWinState(CarController controller) : base(controller)
        {
            GameEvents.CompleteEvent?.Invoke(GetController.gameObject);
            GetController.startPoint.gameObject.SetActive(false);
            GetController.endPoint.gameObject.SetActive(false);
            
            GetController.ChangeState(new CarIdleState(GetController));
        }

        public override void Update()
        {
            
        }

        public override void FixedUpdate()
        {
            
        }
    }
}