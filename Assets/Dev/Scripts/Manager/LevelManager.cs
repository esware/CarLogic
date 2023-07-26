using System;
using ScriptableObjects.Scripts;
using UnityEngine;

 namespace Managers
{
    public struct GameEvents
    {
        public static Action WinEvent;
        public static Action LoseEvent;

        public static void DestroyEvents()
        {
            WinEvent = null;
            LoseEvent = null;
        }
        
    }
    
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private bool transitionWithPrefab;
        [SerializeField] private GameData gameData;

        [HideInInspector] public LevelData levelData;
        
        private const int LevelResetIndex = 1;
        
        private int _currentLevel;

        private int _levelIndex;
        

        private void Awake()
        {
            LoadLevel();
        }

        private void LoadLevel()
        {
            InitLevelData();

            if (transitionWithPrefab)
            {
                InstantiateLevel();
            }
        }

        private void InitLevelData()
        {
            _currentLevel = PlayerPrefs.GetInt("PlayerLevel");

            var lastLevelIndex = gameData.LastLevelIndex;
            _levelIndex = lastLevelIndex == gameData.levelsDataArray.Length - 1 ? LevelResetIndex : lastLevelIndex + 1;
            levelData = gameData.levelsDataArray[_levelIndex];
            
#if UNITY_EDITOR
            Debug.Log($"Current Level: {_currentLevel.ToString()}");
#endif
        }
        
        private static void IncreasePlayerPrefLevel()
        {
            PlayerPrefs.SetInt("PlayerLevel", PlayerPrefs.GetInt("PlayerLevel") + 1);
        }
        private void InstantiateLevel()
        {
            Instantiate(gameData.levelsDataArray[_currentLevel % gameData.levelsDataArray.Length].levelObject);

            levelData = gameData.levelsDataArray[_currentLevel % gameData.levelsDataArray.Length];
        }
        private void OnDestroy()
        {
            GameEvents.DestroyEvents();
        }
    }
}