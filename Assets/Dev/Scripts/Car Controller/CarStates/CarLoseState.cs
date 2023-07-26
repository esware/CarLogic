using UnityEngine;

namespace Dev.Scripts.Car_Controller.CarStates
{
    public class CarLoseState:BaseState
    {
        public CarLoseState(CarController controller) : base(controller)
        {
            Debug.Log("Game Lose ");
        }

        public override void Update()
        {
            
        }

        public override void FixedUpdate()
        {
            
        }
    }
}