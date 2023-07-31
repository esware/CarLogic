using Dev.Scripts;
using UnityEngine;

public abstract class BaseState
{
    protected readonly CarController Controller;
    protected readonly CarMovementRecorder MovementRecorder;
    protected readonly TrailController trailController;

    protected BaseState(CarController controller)
    {
        Controller = controller;
        MovementRecorder = controller.carMovementRecorder;
        trailController = controller.GetComponent<TrailController>();
    }

    public abstract void Update();
    public virtual void FixedUpdate(){}
    public virtual void LateUpdate(){}
    public virtual void OnTriggerEnter(Collider other){}
}