using UnityEngine;
using UnityEngine.UI;
using PrimeTween;

public class DeathIntroController : MonoBehaviour
{
    [Header("Visuals")]
    [SerializeField] private Sprite deathCardSprite;
    [SerializeField] private CanvasGroup canvasGroup;

    [Header("Animation")]
    [SerializeField] private Vector2 cardSize = new(350f, 525f);
    [SerializeField] private float appearDuration = 0.35f;
    [SerializeField] private float displayDuration = 0.7f;
    [SerializeField] private float flyDuration = 1.0f;

    private RectTransform deckTarget;

    private void Awake()
    {
        canvasGroup.alpha = 0f;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.interactable = false;
    }

    private async void Start()
    {
        // Disable card interaction immediately.
        GameManager.Instance.InputEnabled = false;

        // Give the deck and opening cards time to initialise.
        await Awaitable.WaitForSecondsAsync(1f);

        GameObject deckObject = GameObject.Find("Deck");

        if (deckObject == null)
        {
            Debug.LogError(
                "Could not find Deck. Check the object's name in the Hierarchy."
            );

            // Avoid permanently locking the game if setup fails.
            GameManager.Instance.InputEnabled = true;
            return;
        }

        deckTarget = deckObject.GetComponent<RectTransform>();

        if (deckTarget == null)
        {
            Debug.LogError("Deck does not have a RectTransform.");

            GameManager.Instance.InputEnabled = true;
            return;
        }

        await PlayIntro();
    }

    public void SetDeckTarget(RectTransform target)
    {
        deckTarget = target;
    }

    public async Awaitable PlayIntro()
    {
        if (deckTarget == null)
        {
            Debug.LogError("Death intro has no deck target.");
            GameManager.Instance.InputEnabled = true;
            return;
        }

        if (deathCardSprite == null)
        {
            Debug.LogError("No Death card sprite assigned.");
            GameManager.Instance.InputEnabled = true;
            return;
        }

        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.interactable = true;

        GameObject cardObject = new("DeathIntroCard");
        cardObject.transform.SetParent(transform, false);

        Image image = cardObject.AddComponent<Image>();
        image.sprite = deathCardSprite;
        image.preserveAspect = true;
        image.raycastTarget = false;

        RectTransform card = cardObject.GetComponent<RectTransform>();

        card.anchorMin = new Vector2(0.5f, 0.5f);
        card.anchorMax = new Vector2(0.5f, 0.5f);
        card.pivot = new Vector2(0.5f, 0.5f);
        card.anchoredPosition = Vector2.zero;
        card.sizeDelta = cardSize;
        card.localScale = Vector3.zero;
        card.SetAsLastSibling();

        await Tween.Scale(
            card,
            Vector3.one,
            appearDuration,
            Ease.OutBack
        );

        await Awaitable.WaitForSecondsAsync(displayDuration);

        Tween scaleTween = Tween.Scale(
            card,
            Vector3.one * 0.15f,
            flyDuration,
            Ease.InBack
        );

        Tween positionTween = Tween.Position(
            card,
            deckTarget.position,
            flyDuration,
            Ease.InCubic
        );

        await positionTween;

        Destroy(cardObject);

        canvasGroup.alpha = 0f;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.interactable = false;

        // Re-enable card interaction once the intro has finished.
        GameManager.Instance.InputEnabled = true;
    }
}