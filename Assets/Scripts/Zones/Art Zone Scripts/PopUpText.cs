using UnityEngine;
using TMPro;
using PrimeTween;

public class ScorePopup : MonoBehaviour
{
    [SerializeField] private TMP_Text popupText;
    [SerializeField] private CanvasGroup canvasGroup;

    [Header("Animation")]
    [SerializeField] private float riseDistanceY = 80f;
    [SerializeField] private float riseDistanceX = 80f;
    [SerializeField] private float appearDuration = 0.2f;
    [SerializeField] private float displayDuration = 0.8f;
    [SerializeField] private float fadeDuration = 0.35f;

    private RectTransform rectTransform;
    private Vector2 startingPosition;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        startingPosition = rectTransform.anchoredPosition;

        canvasGroup.alpha = 0f;
    }

    public async Awaitable Show(string message)
    {
        gameObject.SetActive(true);

        popupText.text = message;

        rectTransform.anchoredPosition = startingPosition;
        rectTransform.localScale = Vector3.zero;
        canvasGroup.alpha = 1f;

        await Tween.Scale(
            rectTransform,
            Vector3.one,
            appearDuration,
            Ease.OutBack
        );

        await Awaitable.WaitForSecondsAsync(displayDuration);

        _ = Tween.Position(
            rectTransform, 
            rectTransform.position + new Vector3(riseDistanceX, riseDistanceY,0f),
            fadeDuration,
            Ease.OutCubic
            );

        await Tween.Alpha(
            canvasGroup,
            0f,
            fadeDuration
        );

        await Tween.Alpha(
            canvasGroup,
            0f,
            fadeDuration
        );

        gameObject.SetActive(false);
    }
}