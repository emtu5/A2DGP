using System.Collections;
using UnityEngine;

public class FreezeTime : MonoBehaviour
{
    public void Freeze(float duration)
    {
        StartCoroutine(StartFreeze(duration));
    }

    IEnumerator StartFreeze(float duration)
    {
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(duration);
        Time.timeScale = 1f;
    }
}
