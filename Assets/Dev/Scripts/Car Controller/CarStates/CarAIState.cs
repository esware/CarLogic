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
            }
        }

        public override void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Wall"))
            {
                Controller.carMovementRecorder.StopPlayBack();
                trailController.StopTrail();
            }
        
            if (other.gameObject == Controller.endPoint.gameObject)
            {
                Controller.carMovementRecorder.StopPlayBack();
                trailController.StopTrail();
            }

            if (other.CompareTag("Player"))
            {
                Controller.carMovementRecorder.StopPlayBack();
                trailController.StopTrail();
            }
        }

        public CarAIState(CarController controller) : base(controller)
        {
            MovementRecorder.StartPlayback();
            trailController.StartTrail();
        }
    }
}