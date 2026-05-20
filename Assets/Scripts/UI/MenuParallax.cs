using UnityEngine;
using UnityEngine.InputSystem;

public class MenuParallax : MonoBehaviour
{
    public float offsetMultiplier = 50f;
    public float smoothTime = 0.2f;

    private RectTransform rectTransform;
    private Vector2 startPos;
    private Vector2 velocity;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void Start()
    {
        startPos = rectTransform.anchoredPosition;
    }

    void Update()
    {
        if (Mouse.current == null || Camera.main == null) return;

        Vector2 screenPos = Mouse.current.position.ReadValue();
        Vector2 viewportPos = Camera.main.ScreenToViewportPoint(screenPos);

        Vector2 offset = viewportPos - new Vector2(0.5f, 0.5f);

        Vector2 target = startPos + offset * offsetMultiplier;

        rectTransform.anchoredPosition = Vector2.SmoothDamp(
            rectTransform.anchoredPosition,
            target,
            ref velocity,
            smoothTime
        );
    }
}