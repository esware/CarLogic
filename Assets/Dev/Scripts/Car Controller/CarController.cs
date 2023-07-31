using DG.Tweening;
using Managers;
using UnityEngine;
using VHS;

[RequireComponent(typeof(CarMovementRecorder))]
public class CarController : MonoBehaviour
{
    #region Inspector Properties
    [Header("Car Movement Settings")]
    public float speed = 10f;
    public float turnSpeed = 100f;

    [Header("Car Start && Target Points")]
    public Transform startPoint;
    public Transform endPoint;
    
    #endregion

    #region Private Variables

    [HideInInspector] public CarMovementRecorder carMovementRecorder;
    [HideInInspector] public bool isTrackCompleted;

    private Rigidbody _rb;
    private BaseState _currentState;

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
        
        transform.DOLookAt(endPoint.position, 0.5f, AxisConstraint.Y);

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

    private void LateUpdate()
    {
        _currentState.LateUpdate();
    }
    #endregion

    #region State Methods
    public void ChangeState(BaseState newState)
    {
        _currentState = newState;
    }

    public void RecordMovement()
    {
        carMovementRecorder?.RecordMovement(transform.position, transform.rotation);
    }

    public void MoveCar()
    {
        _rb.MovePosition(_rb.position + transform.forward * (speed * Time.deltaTime));

        float turnAmount = InputHandler.Instance.Direction * turnSpeed * Time.deltaTime;
        Quaternion turnRotation = Quaternion.Euler(0f, turnAmount, 0f);

        _rb.MoveRotation(_rb.rotation * turnRotation);
    }
    #endregion

    #region Public Methods
    public void ChangeCarColor(Color color)
    {
        Material newMat = new Material(Shader.Find("Standard"));
        newMat.color = color;

        MeshRenderer carRenderer = transform.GetComponent<MeshRenderer>();
        carRenderer.material = newMat;
    }

    public void IdleState()
    {
        ChangeCarColor(Color.blue);
        carMovementRecorder?.StopPlayBack();
        transform.position = startPoint.position;
        transform.rotation = Quaternion.identity;
    }

    public void WinState()
    {
        startPoint.gameObject.SetActive(false);
        endPoint.gameObject.SetActive(false);
        isTrackCompleted = true;
        GameEvents.CompleteEvent?.Invoke(gameObject);
    }
    #endregion

    #region Collision Handling
    private void OnTriggerEnter(Collider other)
    {
        _currentState.OnTriggerEnter(other);
    }
    #endregion
}
