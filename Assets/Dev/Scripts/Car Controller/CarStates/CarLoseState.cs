using Managers;
using UnityEngine;

namespace Dev.Scripts.Car_Controller.CarStates
{
    public class CarLoseState : BaseState
    {
        public CarLoseState(CarController controller) : base(controller)
        {
            GameEvents.FailEvent?.Invoke();
            trailController.StopTrail();

            if (!controller.isTrackCompleted)
            {
                MovementRecorder.ClearAllRecord();
            }
        }

        public override void Update()
        {
            Controller.ChangeState(new CarIdleState(Controller));
        }
    }
}