using System.Collections.Generic;
using Managers;
using ScriptableObjects.Scripts;
using UnityEngine;

namespace Dev.Scripts.Car_Controller
{
    public class CarManager : MonoBehaviour
    {
        #region Inspector Properties
        [SerializeField] private List<GameObject> npcCar = new List<GameObject>();
        [SerializeField] private GameObject carPrefab;
        #endregion

        #region Private variables
        
        private ILevelDataProvider _levelDataProvider;
        private List<Transform> _startPoints = new List<Transform>();
        private List<Transform> _endPoints = new List<Transform>();

        #endregion

        private void Awake()
        {
            SignUpEvents();
        }

        private void Start()
        {
            Init();
            CreateCar();
        }

        private void Init()
        {
            _levelDataProvider = new LevelDataProvider(FindObjectOfType<LevelGenerator>() );
            _startPoints = _levelDataProvider.GetStartPoints();
            _endPoints = _levelDataProvider.GetEndPoints();

            foreach (var endPoint in _endPoints)
            {
                endPoint.gameObject.SetActive(false);
            }
            foreach (var startPoint in _startPoints)
            {
                startPoint.gameObject.SetActive(false);
            }
        }

        private void SignUpEvents()
        {
            GameEvents.CompleteEvent += CompleteTrack;
            GameEvents.FailEvent += FailTrack;
        }

        #region Custom Methods

        private List<Transform> GetPoints()
        {
            var startPointItem = Random.Range(0, _startPoints.Count);
            var endPointItem = Random.Range(0, _endPoints.Count);
            
            var startPoint = _startPoints[startPointItem];
            var endPoint = _endPoints[endPointItem];
            
            List<Transform> points = new List<Transform>();
            points.Add(startPoint);
            points.Add(endPoint);
            
            _startPoints.Remove(startPoint);
            _endPoints.Remove(endPoint);


            return points;
        }

        private void CreateCar()
        {
            var points = GetPoints();
            var car = Instantiate(carPrefab, points[0].position, Quaternion.identity);
            var carController = car.GetComponent<CarController>();
            carController.startPoint = points[0];
            carController.endPoint = points[1];
        }

        private void CompleteTrack(GameObject car)
        {
            if (npcCar.Count >=_levelDataProvider.GetCarCount()-1)
            {
                Debug.Log("Game Win");
                return;
            }

            npcCar.Add(car);
            CreateCar();
        }

        private void FailTrack()
        {
            foreach (var car in npcCar)
            {
                var carController = car.GetComponent<CarController>();
                carController.ChangeState(new CarIdleState(carController));
            }
        }

        #endregion
    }
}
