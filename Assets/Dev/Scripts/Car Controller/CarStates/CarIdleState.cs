using Dev.Scripts.Car_Controller.CarStates;
using UnityEngine;

public class CarIdleState:BaseState
{
    public CarIdleState(CarController controller) : base(controller)
    {
        controller.IdleState();
    }

    public override void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!Controller.isTrackCompleted)
            {
                Controller.ChangeState(new CarMoveState(Controller));
            }
            else
            {
                Controller.ChangeState(new CarAIState(Controller));
            }
        }
    }

    public override void FixedUpdate()
    {
        
    }
}