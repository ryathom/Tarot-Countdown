using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SacrificeArea : MonoBehaviour
{
    [SerializeField] private RectTransform previewAnchor;
    [SerializeField] private GameObject confirmationPanel;
    [SerializeField] private TMP_Text rewardText;
    [SerializeField] private Button cancelButton;

    private RectTransform rectTransform;
    private Card pendingCard;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        confirmationPanel.SetActive(false);

        cancelButton.onClick.AddListener(CancelSacrifice);
    }

    public bool Contains(Vector2 screenPosition)
    {
        return RectTransformUtility.RectangleContainsScreenPoint(
            rectTransform,
            screenPosition,
            null
        );
    }

    public void ShowConfirmation(Card card)
    {
        pendingCard = card;

        CardContainer container = card.Container;

        container.SetDragging(false);
        container.SetScale(Vector3.one);
        container.ShowPopUp(false);
        container.SetTargetPosition(previewAnchor.position);

        if (card is MinorArcana)
        {
            rewardText.text = $"Sacrifice?\nGain {card.Number / 2} Fate";
        }
        else
        {
            rewardText.text = "Sacrifice?\nReduce Doom by 1";
        }

        confirmationPanel.SetActive(true);
    }

    private void CancelSacrifice()
    {
        if (pendingCard == null)
            return;

        Card cardToReturn = pendingCard;

        pendingCard = null;
        confirmationPanel.SetActive(false);

        cardToReturn.Zone.UpdateVisuals();
    }
}