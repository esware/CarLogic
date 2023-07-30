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
           Controller.RecordMovement();
        }

        public override void FixedUpdate()
        {
            Controller.MoveCar();
            Controller.TurnCar();
        }
    }
}