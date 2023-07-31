using UnityEngine;

namespace Dev.Scripts.Car_Controller.CarStates
{
    public class CarMoveState:BaseState
    {
        public CarMoveState(CarController controller) : base(controller)
        {
            controller.ChangeCarColor(Color.yellow);
            trailController.StartTrail();
        }

        public override void Update()
        {
            
        }

        public override void FixedUpdate()
        {
            Controller.MoveCar();
        }

        public override void LateUpdate()
        {
            Controller.RecordMovement();
        }

        public override void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Wall"))
            {
                Controller.ChangeState(new CarLoseState(Controller));
            }
        
            if (other.gameObject == Controller.endPoint.gameObject)
            {
                Controller.ChangeState(new CarWinState(Controller));
            }

            if (other.CompareTag("Player"))
            {
                Controller.ChangeState(new CarLoseState(Controller));
            }
        }
    }
}