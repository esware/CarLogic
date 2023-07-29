using UnityEngine;

namespace Dev.Scripts.Car_Controller.CarStates
{
    public class CarMoveState:BaseState<CarController>
    {
        public CarMoveState(CarController controller) : base(controller)
        {
            
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

        public override void Exit()
        {
            
        }
    }
}