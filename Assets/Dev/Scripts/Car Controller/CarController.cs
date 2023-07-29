using System;
using Dev.Scripts.Car_Controller.CarStates;
using Managers;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [HideInInspector] public CarMovementRecorder carMovementRecorder;
    [HideInInspector] public bool isTrackCompleted = false;
    private BaseState _currentState;

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
        SignUpEvents();
        Init();
        _currentState = new CarIdleState(this);
    }

    private void Init()
    {
        carMovementRecorder = GetComponent<CarMovementRecorder>();
        _rb = GetComponent<Rigidbody>();
        startPoint.gameObject.SetActive(true);
        endPoint.gameObject.SetActive(true);
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
    
    private void SignUpEvents()
    {
        GameEvents.CompleteEvent += TrackCompleted;
    }

    private void TrackCompleted(GameObject obj)
    {
        if (obj== this.gameObject)
        {
            isTrackCompleted = true;
        }
    }

    public void RecordMovement()
    {
        carMovementRecorder.RecordMovement(transform.position,transform.rotation);
    }

    public void ChangeState(BaseState newState)
    {
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