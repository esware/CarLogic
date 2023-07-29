using UnityEngine;

public abstract class BaseState<T>  where T: MonoBehaviour
{
    protected readonly T Controller;

    protected BaseState(T controller)
    {
        Controller = controller;
    }

    public abstract void Update();
    public abstract void FixedUpdate();
    public abstract void Exit();
}