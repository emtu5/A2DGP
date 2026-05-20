using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
    // Public property so other scripts can read the current shake offset
    public Vector3 CurrentShakeOffset { get; private set; } = Vector3.zero;

    private Coroutine shakeRoutine;
    public void Shake(float duration, float magnitude)
    {
        if (shakeRoutine != null)
            StopCoroutine(shakeRoutine);

        shakeRoutine = StartCoroutine(ShakeRoutine(duration, magnitude));
    }

    private IEnumerator ShakeRoutine(float duration, float magnitude)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-0.3f, 0.3f) * magnitude;
            float y = Random.Range(-0.3f, 0.3f) * magnitude;
            CurrentShakeOffset = new Vector3(x, y, 0);

            elapsed += Time.deltaTime;
            yield return null;
        }

        CurrentShakeOffset = Vector3.zero;
        shakeRoutine = null;
    }
}