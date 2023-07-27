using UnityEngine;

public class TimeManager : MonoBehaviour
{
    private bool timeFrozen = true;

    private void Update()
    {
        if (timeFrozen && Input.touchCount > 0)
        {
            Time.timeScale = 1f;
            timeFrozen = false;
        }
    }

    public void FreezeTime()
    {
        Time.timeScale = 0f;
        timeFrozen = true;
    }
}