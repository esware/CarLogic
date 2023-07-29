using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovementRecorder : MonoBehaviour
{
    public List<Vector3> recordedPositions = new List<Vector3>();
    public List<Quaternion> recordedRotations = new List<Quaternion>();

    public void StartPlayback()
    {
        StartCoroutine(PlaybackMovement());
    }

    public void RecordMovement(Vector3 position, Quaternion rotation)
    {
        recordedPositions.Add(position);
        recordedRotations.Add(rotation);
    }
    IEnumerator PlaybackMovement()
    {
        for (int i = 0; i < recordedPositions.Count; i++)
        {
            Vector3 targetPosition = recordedPositions[i];
            Quaternion targetRotation = recordedRotations[i];
            
            transform.position = targetPosition;
            transform.rotation = targetRotation;

            yield return new WaitForEndOfFrame();
        }
    }
}