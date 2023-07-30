using Managers;
using UnityEngine;

namespace Dev.Scripts.Car_Controller.CarStates
{
    public class CarWinState:BaseState
    {
        public CarWinState(CarController controller) : base(controller)
        {
            controller.WinState();
            trailController.StopTrail();
        }

        public override void Update()
        {
            Controller.ChangeState(new CarIdleState(Controller));
        }

        public override void FixedUpdate()
        {
            
        }
    }
}