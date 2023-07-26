using System;
using Dev.Scripts.Car_Controller.CarStates;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [HideInInspector] public BaseState currentState;
    [HideInInspector] public CarMovementRecorder carMovementRecorder;
    
    public float speed = 10f;
    public float turnSpeed = 100f;

    private Rigidbody rb;
    private bool isGameStart = false;

    #region Unity Methods

    private void Awake()
    {
        carMovementRecorder = GetComponent<CarMovementRecorder>();
        currentState = new CarIdleState(this);
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        currentState.Update();
    }

    private void FixedUpdate()
    {
        currentState.FixedUpdate();
    }

    #endregion
    

    public void ChangeState(BaseState newState)
    {
        currentState = newState;
    }

    public void MoveCar()
    {
        rb.MovePosition(rb.position + transform.forward * speed * Time.deltaTime);
    }

    public void TurnCar()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float turnAmount = horizontalInput * turnSpeed * Time.deltaTime;
        
        Quaternion turnRotation = Quaternion.Euler(0f, turnAmount, 0f);
        rb.MoveRotation(rb.rotation * turnRotation);
    }

    public void CreateCar()
    {
        var clone = Instantiate(this.gameObject, new Vector3(4, 1, -9), Quaternion.identity);
        var clonemovement = clone.GetComponent<CarMovementRecorder>();
        clonemovement.StartPlayback();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Wall")
        {
            ChangeState(new CarLoseState(this));
        }
        
        if (other.tag == "Finish")
        {
            ChangeState(new CarWinState(this));
        }
    }
}