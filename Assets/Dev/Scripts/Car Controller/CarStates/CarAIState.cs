using UnityEngine;

namespace Dev.Scripts.Car_Controller.CarStates
{
    public class CarAIState:BaseState<CarController>
    {
        public CarAIState(CarController controller) : base(controller)
        {
            base.Controller.carMovementRecorder.StartPlayback();
        }

        public override void Update()
        {
            if (Controller.carMovementRecorder.IsPlayBackOver())
            {
                Controller.ChangeState(new CarIdleState(Controller));
            }
        }

        public override void FixedUpdate()
        {
            
        }

        public override void Exit()
        {
            
        }
    }
}