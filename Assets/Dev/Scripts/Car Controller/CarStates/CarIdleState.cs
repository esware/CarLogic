using UnityEngine;

namespace Dev.Scripts.Car_Controller.CarStates
{
    public class CarIdleState:BaseState
    {
        public CarIdleState(CarController controller) : base(controller)
        {
        }

        public override void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                GetController.ChangeState(new CarMoveState(GetController));
                GetController.carMovementRecorder.currentCarIsBeingDriven = true;
            }
        }

        public override void FixedUpdate()
        {
            
        }
    }
}