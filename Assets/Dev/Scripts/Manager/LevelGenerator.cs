using System;
using System.Collections.Generic;
using Dev.Scripts;
using ScriptableObjects.Scripts;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Managers
{
    public class LevelGenerator : MonoBehaviour, ILevelDataProvider
    {
        #region Inspector Properties

        [SerializeField] private int carCount;

        [Space, Header("Add Obstacle")]
        [SerializeField] private GameObject obstaclePrefab;
        [SerializeField] private Transform obstacleParent;
        [SerializeField] private List<GameObject> obstacleList = new List<GameObject>();

        [Space, Header("Add Start Points")]
        [SerializeField] private GameObject startPointPrefab;
        [SerializeField] private Transform startPointsParent;

        [Space, Header("Add End Points")]
        [SerializeField] private GameObject endPointPrefab;
        [SerializeField] private Transform endPointsParent;

        #endregion

        #region List Properties

        public List<Transform> startPointList = new List<Transform>();
        public List<Transform> endPointList = new List<Transform>();

        #endregion

        public void AddObstacle()
        {
            var obstacleClone = Instantiate(obstaclePrefab, Vector3.zero, Quaternion.identity, obstacleParent);
            obstacleList.Add(obstacleClone);
        }

        public void AddStartPoints()
        {
            var startPointClone = Instantiate(startPointPrefab, Vector3.zero, Quaternion.identity, startPointsParent);
            startPointList.Add(startPointClone.transform);
        }

        public void AddEndPoints()
        {
            var endPointClone = Instantiate(endPointPrefab, Vector3.zero, Quaternion.identity, endPointsParent);
            endPointList.Add(endPointClone.transform);
        }

        public void BuildLevel()
        {
#if UNITY_EDITOR
            PrefabUtility.ApplyPrefabInstance(gameObject, InteractionMode.UserAction);
            Debug.Log("Prefab changes applied.");
#endif
        }

        public void ClearLevel()
        {
            foreach (var obstacle in obstacleList)
            {
                DestroyImmediate(obstacle.gameObject);
            }
            obstacleList.Clear();

            foreach (var endPoint in endPointList)
            {
                DestroyImmediate(endPoint.gameObject);
            }
            endPointList.Clear();

            foreach (var startPoint in startPointList)
            {
                DestroyImmediate(startPoint.gameObject);
            }
            startPointList.Clear();
        }

        #region Provider Methods

        public List<Transform> GetStartPoints()
        {
            return startPointList;
        }

        public List<Transform> GetEndPoints()
        {
            return endPointList;
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
            EditorGUILayout.Separator();
            EditorGUILayout.Space(10);

            if (GUILayout.Button("Add Obstacle"))
            {
                levelGenerator.AddObstacle();
            }

            if (GUILayout.Button("Add StartPoints"))
            {
                levelGenerator.AddStartPoints();
            }

            if (GUILayout.Button("Add EndPoints"))
            {
                levelGenerator.AddEndPoints();
            }

            EditorGUILayout.Separator();
            EditorGUILayout.Space(10);

            if (GUILayout.Button("Reset Level"))
            {
                levelGenerator.ClearLevel();
            }

            EditorGUILayout.HelpBox("Build after making all the changes or you will not be able to undo the changes you have made", MessageType.Warning);
            if (GUILayout.Button("Build Level"))
            {
                levelGenerator.BuildLevel();
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
#endif
    #endregion
}
