using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimescaleManager : MonoBehaviour
{
    public void PauseTimescale()
    {
        Time.timeScale = 0f;
    }

    public void UnpauseTimescale()
    {
        Time.timeScale = 1f;
    }

    public void SetTimescale(float timeScale)
    {
        Time.timeScale = timeScale;
    }
}
