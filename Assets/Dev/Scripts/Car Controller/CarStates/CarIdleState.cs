using UnityEngine;

namespace Dev.Scripts.Car_Controller.CarStates
{
    public class CarIdleState:BaseState
    {
        public CarIdleState(CarController controller) : base(controller)
        {
            Time.timeScale = 0f;
        }

        public override void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Time.timeScale = 1f;
                GetController.ChangeState(new CarMoveState(GetController));
                GetController.GetComponent<CarMovementRecorder>().currentCarIsBeingDriven = true;
            }
        }

        public override void FixedUpdate()
        {
            
        }
    }
}