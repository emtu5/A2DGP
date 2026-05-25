using System.Collections;
using UnityEngine;

public class CameraRecoil : MonoBehaviour
{
    public Vector3 CurrentRecoilOffset { get; private set; } = Vector3.zero;

    private Coroutine recoilRoutine;
    public void Recoil(Vector2 direction, float duration)
    {
        if (recoilRoutine != null)
            StopCoroutine(recoilRoutine);

        recoilRoutine = StartCoroutine(RecoilRoutine(direction, duration));
    }

    IEnumerator RecoilRoutine(Vector2 direction, float duration)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            CurrentRecoilOffset = Vector3.Lerp(new Vector3(direction.x, direction.y, 0), Vector3.zero, elapsed / duration);

            elapsed += Time.deltaTime;
            yield return null;
        }

        CurrentRecoilOffset = Vector3.zero;
        recoilRoutine = null;
    }
}
