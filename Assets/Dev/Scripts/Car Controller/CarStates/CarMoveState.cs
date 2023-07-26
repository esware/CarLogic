namespace Dev.Scripts.Car_Controller.CarStates
{
    public class CarMoveState:BaseState
    {
        public CarMoveState(CarController controller) : base(controller)
        {
        }

        public override void Update()
        {
           
        }

        public override void FixedUpdate()
        {
            GetController.MoveCar();
            GetController.TurnCar();
        }
    }
}