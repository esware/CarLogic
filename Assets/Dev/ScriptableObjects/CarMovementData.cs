using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Car MovementData",menuName = "ScriptableObjects/CarData")]
public class CarMovementData : ScriptableObject
{
    public List<Vector3> recordedPositions = new List<Vector3>();
    public List<Quaternion> recordedRotations = new List<Quaternion>();
    
}
