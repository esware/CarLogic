using UnityEngine;

namespace Dev.Scripts.Car_Controller.CarStates
{
    public class CarWinState:BaseState
    {
        public CarWinState(CarController controller) : base(controller)
        {
            GetController.ResetPositionAndRotation();
            GetController.GetComponent<CarMovementRecorder>().currentCarIsBeingDriven = false;
        }

        public override void Update()
        {
            
        }

        public override void FixedUpdate()
        {
            
        }
    }
}