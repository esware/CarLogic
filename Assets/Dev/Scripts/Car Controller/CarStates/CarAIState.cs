using UnityEngine;

namespace Dev.Scripts.Car_Controller.CarStates
{
    public class CarAIState:BaseState
    {
        public CarAIState(CarController controller) : base(controller)
        {
            GetController.carMovementRecorder.StartPlayback();
            Debug.Log("Start Playback");
        }

        public override void Update()
        {
        }

        public override void FixedUpdate()
        {
            
        }
    }
}