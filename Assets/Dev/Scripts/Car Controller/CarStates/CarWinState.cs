using Managers;
using UnityEngine;

namespace Dev.Scripts.Car_Controller.CarStates
{
    public class CarWinState:BaseState<CarController>
    {
        public CarWinState(CarController controller) : base(controller)
        {
            controller.WinState();
        }

        public override void Update()
        {
            Controller.ChangeState(new CarIdleState(Controller));
        }

        public override void FixedUpdate()
        {
            
        }

        public override void Exit()
        {
            
        }
    }
}