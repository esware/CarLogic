using System.Collections.Generic;
using Managers;
using ScriptableObjects.Scripts;
using UnityEngine;

namespace Dev.Scripts.Car_Controller
{
    public class CarManager : MonoBehaviour
    {
        private ILevelDataProvider _levelDataProvider;

        public List<GameObject> npcCar = new List<GameObject>();
        public GameObject carPrefab;

        private List<Transform> _startPoints = new List<Transform>();
        private List<Transform> _endPoints = new List<Transform>();
        private int _carCounter = 0;

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

        private void CreateCar()
        {
            var car = Instantiate(carPrefab, _startPoints[0].position, Quaternion.identity);
            var carController = car.GetComponent<CarController>();
            carController.startPoint = _startPoints[0];
            carController.endPoint = _endPoints[0];
            car.gameObject.name = _carCounter.ToString();
        }

        private void CompleteTrack(GameObject car)
        {
            if (npcCar.Count >=_levelDataProvider.GetCarCount()-1)
            {
                Debug.Log("Game Win");
                return;
            }
            _carCounter++;
            var startPoint = _startPoints[_carCounter];
            npcCar.Add(car);

            var newCar = Instantiate(carPrefab, startPoint.position, Quaternion.identity);
            newCar.GetComponent<CarController>().startPoint = startPoint;
            newCar.GetComponent<CarController>().endPoint = _endPoints[_carCounter];
            
        }

        private void FailTrack()
        {
            // Eğer başarısızlık durumunda yapılacak işlemler varsa buraya ekleyebilirsiniz.
        }
    }
}
