using System;
using Dev.Scripts.Car_Controller.CarStates;
using Managers;
using UnityEngine;

public class CarController : MonoBehaviour
{
    private BaseState<CarController> _currentState;
    
    [HideInInspector] public CarMovementRecorder carMovementRecorder;
     public bool isTrackCompleted = false;

    [Space,Header("Car Movement Settings")]
    public float speed = 10f;
    public float turnSpeed = 100f;

    [Space,Header("Car Start && Target Points")]
    public Transform startPoint;
    public Transform endPoint;
    

    private Rigidbody _rb;

    #region Unity Methods
    
    private void Start()
    {
        Init();
    }

    private void Init()
    {
        carMovementRecorder = GetComponent<CarMovementRecorder>();
        startPoint.gameObject.SetActive(true);
        endPoint.gameObject.SetActive(true);
        _rb = GetComponent<Rigidbody>();
        _currentState = new CarIdleState(this);
    }
    

    private void Update()
    {
        _currentState.Update();
    }

    private void FixedUpdate()
    {
        _currentState.FixedUpdate();
    }

    #endregion
    
    public void WinState()
    {
        startPoint.gameObject.SetActive(false);
        endPoint.gameObject.SetActive(false);
        isTrackCompleted = true;
        GameEvents.CompleteEvent?.Invoke(gameObject);
    }

    public void RecordMovement()
    {
        carMovementRecorder.RecordMovement(transform.position,transform.rotation);
    }

    public void ChangeState(BaseState<CarController> newState)
    {
        _currentState.Exit();
        _currentState = newState;
    }

    public void MoveCar()
    {
        _rb.MovePosition(_rb.position + transform.forward *(speed * Time.deltaTime));
    }

    public void TurnCar()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float turnAmount = horizontalInput * turnSpeed * Time.deltaTime;
        
        Quaternion turnRotation = Quaternion.Euler(0f, turnAmount, 0f);
        _rb.MoveRotation(_rb.rotation * turnRotation);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Wall"))
        {
            ChangeState(new CarLoseState(this));
        }
        
        if (other.gameObject == endPoint.gameObject)
        {
            ChangeState(new CarWinState(this));
        }
    }
    
}