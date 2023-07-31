using System.Collections.Generic;
using Managers;
using ScriptableObjects.Scripts;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Dev.Scripts.Car_Controller
{
    public class CarManager : MonoBehaviour
    {
        #region Inspector Properties
        [SerializeField] private List<GameObject> npcCar = new List<GameObject>();
        [SerializeField] private GameObject carPrefab;
        #endregion

        #region Private Variables
        private ILevelDataProvider _levelDataProvider;
        private List<Transform> _startPoints = new List<Transform>();
        private List<Transform> _endPoints = new List<Transform>();
        #endregion

        #region Unity Callbacks
        private void Awake()
        {
            SignUpEvents();
        }

        private void Start()
        {
            Init();
            CreateCar();
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }
        #endregion

        #region Initialization and Events
        private void Init()
        {
            _levelDataProvider = new LevelDataProvider(FindObjectOfType<LevelGenerator>());
            _startPoints = _levelDataProvider.GetStartPoints();
            _endPoints = _levelDataProvider.GetEndPoints();

            SetActiveAll(_endPoints, false);
            SetActiveAll(_startPoints, false);
        }

        private void SignUpEvents()
        {
            GameEvents.CompleteEvent += CompleteTrack;
            GameEvents.FailEvent += FailTrack;
        }

        private void UnsubscribeEvents()
        {
            GameEvents.CompleteEvent -= CompleteTrack;
            GameEvents.FailEvent -= FailTrack;
        }
        #endregion

        #region Custom Methods
        private List<Transform> GetPoints()
        {
            int startPointItem = Random.Range(0, _startPoints.Count - 1);
            int endPointItem = Random.Range(0, _endPoints.Count - 1);

            Transform startPoint = _startPoints[startPointItem];
            Transform endPoint = _endPoints[endPointItem];

            List<Transform> points = new List<Transform>();
            points.Add(startPoint);
            points.Add(endPoint);

            _startPoints.Remove(startPoint);
            _endPoints.Remove(endPoint);

            return points;
        }

        private void CreateCar()
        {
            List<Transform> points = GetPoints();
            GameObject car = Instantiate(carPrefab, points[0].position, Quaternion.identity);
            CarController carController = car.GetComponent<CarController>();
            carController.startPoint = points[0];
            carController.endPoint = points[1];
        }

        private void CompleteTrack(GameObject car)
        {
            if (npcCar.Count >= _levelDataProvider.GetCarCount() - 1)
            {
                GameEvents.WinEvent?.Invoke();
                return;
            }

            npcCar.Add(car);

            if (npcCar.Count > 0)
            {
                foreach (GameObject c in npcCar)
                {
                    CarController carController = c.GetComponent<CarController>();
                    carController.ChangeState(new CarIdleState(carController));
                }
            }
            CreateCar();
        }

        private void FailTrack()
        {
            if (npcCar.Count > 0)
            {
                foreach (GameObject car in npcCar)
                {
                    if (car == null)
                    {
                        return;
                    }
                    CarController carController = car.GetComponent<CarController>();
                    if (carController != null)
                    {
                        carController.ChangeState(new CarIdleState(carController));
                    }
                    else
                    {
                        car.AddComponent<CarController>().ChangeState(new CarIdleState(carController));
                    }
                }
            }
        }

        private void SetActiveAll(List<Transform> objects, bool isActive)
        {
            foreach (var obj in objects)
            {
                obj.gameObject.SetActive(isActive);
            }
        }
        #endregion
    }
}
