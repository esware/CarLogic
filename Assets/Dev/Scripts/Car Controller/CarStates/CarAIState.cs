using UnityEngine;

namespace Dev.Scripts.Car_Controller.CarStates
{
    public class CarAIState:BaseState
    {
        public override void Update()
        {
            if (Controller.carMovementRecorder.IsPlayBackOver)
            {
                Controller.carMovementRecorder.IsPlayBackOver = false;
                Controller.ChangeState(new CarIdleState(Controller));
            }
        }

        public override void FixedUpdate()
        {
            
        }

        public CarAIState(CarController controller) : base(controller)
        {
            MovementRecorder.StartPlayback();
        }
    }
}