using UnityEngine;

namespace Dev.Scripts.Car_Controller.CarStates
{
    public class CarWinState:BaseState
    {
        public CarWinState(CarController controller) : base(controller)
        {
            Debug.Log("Game win ");
            GetController.CreateCar();
        }

        public override void Update()
        {
            
        }

        public override void FixedUpdate()
        {
            
        }
    }
}