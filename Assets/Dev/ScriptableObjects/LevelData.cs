using Unity.VisualScripting;
using UnityEngine;

namespace ScriptableObjects.Scripts
{
    [CreateAssetMenu(fileName = "LevelData", menuName = "ScriptableObjects/LevelData", order = 1)]
    public class LevelData : ScriptableObject
    {
        [Header("Level")] [SerializeField] public GameObject levelObject;
        [SerializeField] private int CarCount;
        [SerializeField] private GameObject[] carPrefabs;
        

    }
}