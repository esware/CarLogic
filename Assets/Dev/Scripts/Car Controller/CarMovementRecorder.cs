using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovementRecorder : MonoBehaviour
{
    public CarMovementData movementData;

    public bool currentCarIsBeingDriven;

    public float playbackInterval = 1f;
    
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
            // Use the data to animate the car's movement during playback
            transform.position = movementData.recordedPositions[i];
            transform.rotation = movementData.recordedRotations[i];

            // Pause for a short time to control playback speed
            yield return new WaitForSeconds(playbackInterval);
        }
    }

}