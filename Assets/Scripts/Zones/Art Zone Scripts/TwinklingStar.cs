using UnityEngine;
using UnityEngine.UI;

public class TwinklingStar : MonoBehaviour
{
    [Header("Brightness")]
    [SerializeField]
    private Vector2 minimumAlphaRange =
        new Vector2(0.15f, 0.4f);

    [SerializeField]
    private Vector2 maximumAlphaRange =
        new Vector2(0.65f, 1f);

    [Header("Size")]
    [SerializeField]
    private Vector2 minimumScaleRange =
        new Vector2(0.85f, 0.95f);

    [SerializeField]
    private Vector2 maximumScaleRange =
        new Vector2(1.05f, 1.15f);

    [Header("Speed")]
    [SerializeField]
    private Vector2 twinkleSpeedRange =
        new Vector2(0.4f, 1.3f);

    [Header("Colour variation")]
    [SerializeField] private bool useColourVariation = true;

    [Header("Rare magical pulse")]
    [SerializeField] private bool enableMagicalPulse = true;
    [SerializeField]
    private Vector2 pulseDelayRange =
        new Vector2(30f, 90f);

    [SerializeField] private float pulseDuration = 2f;
    [SerializeField] private float pulseStrength = 0.25f;

    private Image starImage;

    private float minimumAlpha;
    private float maximumAlpha;
    private float minimumScale;
    private float maximumScale;
    private float twinkleSpeed;
    private float randomOffset;

    private float nextPulseTime;
    private float pulseStartTime;
    private bool isPulsing;

    private Color baseColour;

    private void Awake()
    {
        starImage = GetComponent<Image>();

        minimumAlpha = Random.Range(
            minimumAlphaRange.x,
            minimumAlphaRange.y
        );

        maximumAlpha = Random.Range(
            maximumAlphaRange.x,
            maximumAlphaRange.y
        );

        minimumScale = Random.Range(
            minimumScaleRange.x,
            minimumScaleRange.y
        );

        maximumScale = Random.Range(
            maximumScaleRange.x,
            maximumScaleRange.y
        );

        twinkleSpeed = Random.Range(
            twinkleSpeedRange.x,
            twinkleSpeedRange.y
        );

        randomOffset = Random.Range(
            0f,
            Mathf.PI * 2f
        );

        baseColour = starImage.color;

        if (useColourVariation)
        {
            ApplySubtleColourVariation();
        }

        ScheduleNextPulse();
    }

    private void Update()
    {
        UpdateTwinkle();

        if (enableMagicalPulse)
        {
            UpdateMagicalPulse();
        }
    }

    private void UpdateTwinkle()
    {
        float wave =
            (Mathf.Sin(Time.time * twinkleSpeed + randomOffset) + 1f)
            / 2f;

        float alpha = Mathf.Lerp(
            minimumAlpha,
            maximumAlpha,
            wave
        );

        float scale = Mathf.Lerp(
            minimumScale,
            maximumScale,
            wave
        );

        Color colour = baseColour;
        colour.a = alpha;

        starImage.color = colour;
        transform.localScale = Vector3.one * scale;
    }

    private void UpdateMagicalPulse()
    {
        if (!isPulsing && Time.time >= nextPulseTime)
        {
            isPulsing = true;
            pulseStartTime = Time.time;
        }

        if (!isPulsing)
        {
            return;
        }

        float progress =
            (Time.time - pulseStartTime) / pulseDuration;

        if (progress >= 1f)
        {
            isPulsing = false;
            ScheduleNextPulse();
            return;
        }

        float pulse =
            Mathf.Sin(progress * Mathf.PI);

        Color colour = starImage.color;
        colour.a = Mathf.Clamp01(
            colour.a + pulse * pulseStrength
        );

        starImage.color = colour;

        transform.localScale *=
            1f + pulse * 0.15f;
    }

    private void ApplySubtleColourVariation()
    {
        int variation = Random.Range(0, 3);

        switch (variation)
        {
            case 0:
                // Slightly warm
                baseColour = new Color(1f, 0.97f, 0.9f);
                break;

            case 1:
                // Slightly cool
                baseColour = new Color(0.9f, 0.95f, 1f);
                break;

            default:
                // Neutral white
                baseColour = Color.white;
                break;
        }
    }

    private void ScheduleNextPulse()
    {
        nextPulseTime =
            Time.time +
            Random.Range(
                pulseDelayRange.x,
                pulseDelayRange.y
            );
    }
}