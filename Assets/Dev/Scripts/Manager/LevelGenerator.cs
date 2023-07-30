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
        public Vector3 areaSize;
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
                        obstacle.transform.localScale = new Vector3(1,5,areaSize.x);
                        obstacle.transform.position = new Vector3(areaSize.x/2, 1, 0);
                    }
                }
                else
                {
                    obstacle.transform.localScale = new Vector3(1,5,areaSize.x);
                    obstacle.transform.position = new Vector3(-areaSize.x/2, 1, 0);
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

            float cubeSize = areaSize.x / carCount;
            float halfSideLength = areaSize.x / 2f;
            float halfCubeSize = cubeSize / 2f;

            for (int i = 0; i < carCount; i++)
            {
                float x = -halfSideLength + i * cubeSize + halfCubeSize;
                float z = -halfSideLength +0* cubeSize + halfCubeSize;
                Vector3 position = new Vector3(x, 0f, z);
                
                if (Mathf.Abs(position.x) < halfSideLength && Mathf.Abs(position.z) < halfSideLength)
                {
                    var startPointGameObject = Instantiate(startPointPrefab, position, Quaternion.identity, _startPointsParent.transform);
                    startPointGameObject.gameObject.name = "Baslangic Noktasi " + (i + 1);

                    startPoints.Add(startPointGameObject.transform);
                    levelPoints.Add(startPointGameObject);
                }
            }

            for (int i = 0; i < carCount; i++)
            {
                float x = -halfSideLength + i * cubeSize + halfCubeSize;
                float z = halfSideLength +0* cubeSize - halfCubeSize;
                Vector3 position = new Vector3(x, 0f, z);
                
                var endPointGameObject = Instantiate(endPointPrefab, position, Quaternion.identity, _endPointsParent.transform);
                endPointGameObject.gameObject.name = "Bitis Noktasi " + (i + 1);

                endPoints.Add(endPointGameObject.transform);
                levelPoints.Add(endPointGameObject);
            }
           
        }
        
        
        public void ClearLevel()
        {
            startPoints.Clear();
            endPoints.Clear();
            levelPoints.Clear();
            _obstacleList.Clear();
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
#if UNITY_EDITOR
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
#endif
    #endregion


}