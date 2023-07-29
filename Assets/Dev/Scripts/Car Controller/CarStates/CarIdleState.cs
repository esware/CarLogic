using Managers;
using UnityEngine;

namespace Dev.Scripts.Car_Controller.CarStates
{
    public class CarIdleState:BaseState
    {
        public CarIdleState(CarController controller) : base(controller)
        {
            var transform = GetController.transform;
            transform.position = GetController.startPoint.position;
            transform.rotation = Quaternion.identity;
        }

        public override void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log(GetController.isTrackCompleted);
                if (!GetController.isTrackCompleted)
                {
                    GetController.ChangeState(new CarMoveState(GetController));
                }
                else
                {
                    Debug.Log("Completed"+GetController.gameObject.name);
                    GetController.ChangeState(new CarAIState(GetController));
                }
            }
        }

        public override void FixedUpdate()
        {
            
        }
    }
}