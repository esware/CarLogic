using Dev.Scripts.Car_Controller.CarStates;
using UnityEngine;

public class CarIdleState:BaseState<CarController>
{
    public CarIdleState(CarController controller) : base(controller)
    {
        var transform1 = controller.transform;
        transform1.position = controller.startPoint.position;
        transform1.rotation = Quaternion.identity;
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

    public override void Exit()
    {
        
    }
}