using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ShootingStar : MonoBehaviour
{
    [Header("Travel Duration")]
    [SerializeField] private float minimumDuration = 1000f;
    [SerializeField] private float maximumDuration = 2000f;

    [Header("Scale")]
    [SerializeField] private Vector2 startScale = new Vector2(1.33f, 0.76f);
    [SerializeField] private Vector2 endScale = new Vector2(4.0f, 0.22f);

    private RectTransform rect;
    private Image image;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
        image = GetComponent<Image>();
    }

    public void Launch(Vector2 startPosition, Vector2 endPosition)
    {
        rect.anchoredPosition = startPosition;

        Vector2 direction = endPosition - startPosition;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        rect.localRotation = Quaternion.Euler(0f, 0f, angle);

        // Each shooting star receives its own random travel duration.
        float duration = Random.Range(minimumDuration, maximumDuration);

        StartCoroutine(Fly(startPosition, endPosition, duration));
    }

    private IEnumerator Fly(
        Vector2 startPosition,
        Vector2 endPosition,
        float duration)
    {
        float elapsed = 0f;

        Vector3 initialScale = new Vector3(
            startScale.x,
            startScale.y,
            1f
        );

        Vector3 finalScale = new Vector3(
            endScale.x,
            endScale.y,
            1f
        );

        rect.localScale = initialScale;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            float progress = Mathf.Clamp01(elapsed / duration);

            // Travel across the screen.
            rect.anchoredPosition = Vector2.Lerp(
                startPosition,
                endPosition,
                progress
            );

            // Become longer and thinner while travelling.
            rect.localScale = Vector3.Lerp(
                initialScale,
                finalScale,
                progress
            );

            // Fade in and then fade out.
            Color colour = image.color;
            colour.a = Mathf.Sin(progress * Mathf.PI);
            image.color = colour;

            yield return null;
        }

        Destroy(gameObject);
    }
}