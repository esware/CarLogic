using System;
using System.Collections.Generic;
using Dev.Scripts;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Managers
{
    public class LevelGenerator:MonoBehaviour,ILevelDataProvider
    {
        
        [HideInInspector] public List<Transform> startPoints = new List<Transform>();
        [HideInInspector] public List<Transform> endPoints = new List<Transform>();

        #region Inspector

        [Space, Header("Generate Level")] [Space, Header("Level Properties")]
        public int carCount;
        public Vector2 gameAreaSize;
        public GameObject startPointPrefab;
        public GameObject endPointPrefab;
        
        [Space, Header("Add Obstacle")] [SerializeField]
        private GameObject obstaclePrefab;
        
        [Space,Header("Created Points List")]
        public List<GameObject> levelPoints = new List<GameObject>();

        #endregion


        #region Private Properties

        private List<GameObject> _obstacleList = new List<GameObject>();
        private GameObject _startPointsParent;
        private GameObject _endPointsParent;
        private GameObject _carParent;
        private GameObject _obstacleParent;

        #endregion

        private void Awake()
        {
            CreateLevelPoints();
            GenerateObstacles();
        }

        public void GenerateObstacles()
        {
            _obstacleParent = new GameObject("Obstacle Parent");
            _obstacleParent.transform.SetParent(transform);
            
            for (int i = 0; i < carCount; i++)
            {

                var zPos = Random.Range(startPoints[i].position.z+8, endPoints[i].position.z-8);
                var obstaclePosition = new Vector3(startPoints[i].position.x, 1, zPos);

                GameObject obstacle = Instantiate(obstaclePrefab, obstaclePosition, Quaternion.identity,_obstacleParent.transform);
                
                if (_obstacleList.Count>0)
                {
                    obstacle.transform.localScale = Obstacle.CalculateScale(_obstacleList[i - 1].transform.localScale);

                    if (i == carCount-1)
                    {
                        obstacle.transform.localScale = new Vector3(1,1,30);
                        obstacle.transform.position = new Vector3(startPoints[i].position.x, 1, 0);
                    }
                }
                else
                {
                    obstacle.transform.localScale = new Vector3(1,1,30);
                    obstacle.transform.position = new Vector3(startPoints[i].position.x, 1, 0);
                }
                
                
                _obstacleList.Add(obstacle);
            }
        }

        public void CreateLevelPoints()
        {

            _startPointsParent = new GameObject();
            _startPointsParent.gameObject.name = "startPositionParent";
            _startPointsParent.transform.SetParent(transform);

            _endPointsParent = new GameObject();
            _endPointsParent.gameObject.name = "endPositionParent";
            _endPointsParent.transform.SetParent(transform);

            var distanceBetweenTwoPoints = gameAreaSize.x / (carCount-1);
            for (int i = 0; i < carCount; i++)
            {
                var startPointPosition = new Vector3(((-gameAreaSize.x/2 ) + (distanceBetweenTwoPoints)*i), 1,(-gameAreaSize.y/2 ));
                
                
                var startPointGameObject = Instantiate(startPointPrefab, startPointPosition, Quaternion.identity, _startPointsParent.transform);
                startPointGameObject.gameObject.name = "Baslangic Noktasi " + (i + 1);
                
                startPoints.Add(startPointGameObject.transform);
                levelPoints.Add(startPointGameObject);
            }
            
            for (int i = 0; i < carCount; i++)
            {
                var endPointPosition = new Vector3((gameAreaSize.x/2 ) - (distanceBetweenTwoPoints)*i, 1,(gameAreaSize.y/2 ));
                
                var endPointGameObject = Instantiate(endPointPrefab, endPointPosition, Quaternion.identity, _endPointsParent.transform);
                endPointGameObject.gameObject.name = "Bitis Noktasi " + (i + 1);
                
                endPoints.Add(endPointGameObject.transform);
                levelPoints.Add(endPointGameObject);
            }
        }
        
        public void ClearLevel()
        {
            levelPoints.Clear();
            _obstacleList.Clear();
            DestroyImmediate(_carParent.gameObject);
            DestroyImmediate(_endPointsParent.gameObject);
            DestroyImmediate(_startPointsParent.gameObject);
            DestroyImmediate(_obstacleParent.gameObject);
        }

        #region Provider Methods

        public List<Transform> GetStartPoints()
        {

            return startPoints;
        }
        
        public List<Transform> GetEndPoints()
        {
            return endPoints ;
        }

        public int GetCarCount()
        {
            return carCount;
        }

        #endregion
    }

    #region Editor

    [CustomEditor(typeof(LevelGenerator))]
    public class LevelGeneratorEditor : Editor
    {
        private LevelGenerator levelGenerator;

        private void OnEnable()
        {
            levelGenerator = (LevelGenerator)target;
        }

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            serializedObject.Update();
        

            if (GUILayout.Button("Create Level"))
            {
                levelGenerator.CreateLevelPoints();
                levelGenerator.GenerateObstacles();
            }

            if (GUILayout.Button("Reset Level"))
            {
                levelGenerator.ClearLevel();
            }

            serializedObject.ApplyModifiedProperties();
        }
    }

        #endregion


}