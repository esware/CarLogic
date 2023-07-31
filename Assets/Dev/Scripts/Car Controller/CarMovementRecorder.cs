using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovementRecorder : MonoBehaviour
{
    #region Inspector Properties
    public List<Vector3> recordedPositions = new List<Vector3>();
    public List<Quaternion> recordedRotations = new List<Quaternion>();
    #endregion

    #region Private Variables
    private bool _isPlayBackOver = false;
    private Coroutine _playbackCoroutine;
    #endregion

    #region Properties
    public bool IsPlayBackOver
    {
        get => _isPlayBackOver;
        set => _isPlayBackOver = value;
    }
    #endregion

    #region Custom Methods
    public void StartPlayback()
    {
        _playbackCoroutine = StartCoroutine(PlaybackMovement());
    }

    public void StopPlayBack()
    {
        if (_playbackCoroutine != null)
        {
            StopCoroutine(_playbackCoroutine);
            _playbackCoroutine = null;
        }
    }

    public void ClearAllRecord()
    {
        recordedPositions.Clear();
        recordedRotations.Clear();
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
        _isPlayBackOver = true;
    }
    #endregion
}
