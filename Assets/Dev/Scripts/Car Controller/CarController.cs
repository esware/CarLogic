using System;
using Dev.Scripts.Car_Controller.CarStates;
using Managers;
using UnityEngine;

[RequireComponent(typeof(CarMovementRecorder))]
public class CarController : MonoBehaviour
{
    private BaseState _currentState;
    private Rigidbody _rb;
    
    [HideInInspector] public CarMovementRecorder carMovementRecorder;
    public bool isTrackCompleted = false;

    #region Inspector Properties

    [Space,Header("Car Movement Settings")]
    public float speed = 10f;
    public float turnSpeed = 100f;

    [Space,Header("Car Start && Target Points")]
    public Transform startPoint;
    public Transform endPoint;

    #endregion
    

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

    #region State Methods

    public void IdleState()
    {
        ChangeCarColor(Color.blue);
        carMovementRecorder.StopPlayBack();
        var transform1 = transform;
        transform1.position = startPoint.position;
        transform1.rotation = Quaternion.identity;
    }

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

    public void ChangeState(BaseState newState)
    {
        _currentState = newState;
    }

    public void ChangeCarColor(Color color)
    {
        Material newMat = new Material(Shader.Find("Standard"));
        newMat.color = color;
        
        MeshRenderer carRenderer = transform.GetComponent<MeshRenderer>();
        carRenderer.material = newMat;
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

    #endregion
    
    
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

        if (other.CompareTag("Player"))
        {
            ChangeState(new CarLoseState(this));
        }
    }
    
}