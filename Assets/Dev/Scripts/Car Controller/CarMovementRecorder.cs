using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovementRecorder : MonoBehaviour
{
    public CarMovementData movementData;

    public bool currentCarIsBeingDriven;

    void Update()
    {
        if (currentCarIsBeingDriven)
        {
            RecordMovement(transform.position, transform.rotation);
        }
    }

    void RecordMovement(Vector3 position, Quaternion rotation)
    {
        movementData.recordedPositions.Add(position);
        movementData.recordedRotations.Add(rotation);
    }

    public void StartPlayback()
    {
        StartCoroutine(PlaybackMovement());
    }

    IEnumerator PlaybackMovement()
    {
        for (int i = 0; i < movementData.recordedPositions.Count; i++)
        {
            Vector3 targetPosition = movementData.recordedPositions[i];
            Quaternion targetRotation = movementData.recordedRotations[i];
            
            transform.position = targetPosition;
            transform.rotation = targetRotation;

            yield return new WaitForEndOfFrame();
        }
    }
}