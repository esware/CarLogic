using System;
using Dev.Scripts.Car_Controller.CarStates;
using UnityEngine;

public class CarController : MonoBehaviour
{
    private BaseState _currentState;

    [Space,Header("Car Movement Settings")]
    public float speed = 10f;
    public float turnSpeed = 100f;

    [Space,Header("Car Start && Target Points")]
    public Transform startPosition;
    public Transform endPosition;
    

    private Rigidbody _rb;

    #region Unity Methods

    private void Awake()
    {
        _currentState = new CarIdleState(this);
    }

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        _rb = GetComponent<Rigidbody>();
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

    private void OnEnable()
    {
        transform.position = startPosition.position;
    }
    

    public void ResetPositionAndRotation()
    {
        transform.position = startPosition.position;
        transform.rotation = Quaternion.identity;
        
        startPosition.gameObject.SetActive(false);
        endPosition.gameObject.SetActive(false);
        
        gameObject.GetComponent<CarMovementRecorder>().StartPlayback();
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
        
        if (other.CompareTag("Finish"))
        {
            ChangeState(new CarWinState(this));
        }
    }


    private void InstantinateCar()
    {
        //var carClone = Instantiate(carPrefab,)
    }
}