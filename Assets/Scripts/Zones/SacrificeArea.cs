using System.Collections.Generic;
using PrimeTween;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SacrificeArea : Zone, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Zones")]
    [SerializeField] private HandArea handArea;

    [Header("UI")]
    [SerializeField] private GameObject confirmationPanel;
    [SerializeField] private TMP_Text rewardText;
    [SerializeField] private Button confirmButton;
    [SerializeField] private Button cancelButton;
    [SerializeField] private Image zoneImage;
    [SerializeField] private Color emptyZoneColor = Color.red;
    [SerializeField] private GameObject sacrificePrompt;
    [SerializeField] private GameObject sacrificeLockedPanel;
    [SerializeField] private CanvasGroup ritualCanvasGroup;

    [Header("Pentagram")]
    [SerializeField] private List<SacrificePoint> sacrificePoints;
    [SerializeField] private Color occupiedPointColor = Color.red;

    [Header("Card Preview")]
    [SerializeField] private RectTransform cardPreviewAnchor;
    [SerializeField] private float previewScale = 0.8f;

    private Card previewedCard;
    private bool isPointerOverArea;

    private const int MaximumCards = 5;

    public bool HasPendingCards
    {
        get { return Cards.Count > 0; }
    }

    protected override void Start()
    {
        base.Start();

        if (sacrificePrompt != null)
        {
            sacrificePrompt.transform.localScale = Vector3.zero;
        }

        if (zoneImage != null)
        {
            zoneImage.color = emptyZoneColor;
        }

        UpdateConfirmationPanel();

        if (confirmButton != null)
        {
            confirmButton.onClick.AddListener(ConfirmSacrifice);
        }

        if (cancelButton != null)
        {
            cancelButton.onClick.AddListener(CancelSacrifice);
        }

        foreach (SacrificePoint point in sacrificePoints)
        {
            point.HoverEntered += ShowCardPreview;
            point.HoverExited += HideCardPreview;
            point.Clear();
        }
        UpdateSacrificeAvailability();
    }

    public bool CanAddCard(Card card)
    {
        if (GameManager.Instance.CanSacrifice == false)
            return false;

        if (card is not MinorArcana)
            return false;

        return Cards.Count < MaximumCards;
    }

    public override void AddCard(Card card)
    {
        base.AddCard(card);

        card.Container.SetScale(Vector3.one);

        ShowSacrificePrompt(false);

        SoundFXManager.Instance.PlaySacrificePointSound(
            Cards.Count - 1,
            transform
        );

        UpdateConfirmationPanel();
    }

    public void UpdateSacrificeAvailability()
    {
        bool canSacrifice = GameManager.Instance.CanSacrifice;

        if (sacrificeLockedPanel != null)
        {
            sacrificeLockedPanel.SetActive(!canSacrifice);
        }

        if (ritualCanvasGroup != null)
        {
            ritualCanvasGroup.alpha = canSacrifice ? 1f : 0.35f;
        }

        if (!canSacrifice)
        {
            ShowSacrificePrompt(false);
        }
        else
        {
            UpdateSacrificePrompt();
        }
    }
    private void UpdateSacrificePrompt()
    {
        bool shouldShow =
            isPointerOverArea &&
            Cards.Count == 0 &&
            GameManager.Instance.CanSacrifice;

        ShowSacrificePrompt(shouldShow);
    }

    public override void RemoveCard(Card card)
    {
        if (previewedCard == card)
        {
            previewedCard = null;
        }

        base.RemoveCard(card);

        UpdateConfirmationPanel();
        UpdateSacrificePrompt();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isPointerOverArea = true;
        UpdateSacrificePrompt();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isPointerOverArea = false;
        ShowSacrificePrompt(false);
    }

    private void ShowSacrificePrompt(bool enabled)
    {
        if (sacrificePrompt == null)
            return;

        Tween.Scale(
            sacrificePrompt.transform,
            enabled ? Vector3.one : Vector3.zero,
            0.1f
        );
    }

    private void UpdateConfirmationPanel()
    {
        int cardCount = Cards.Count;
        bool hasCards = cardCount > 0;

        if (confirmationPanel != null)
        {
            confirmationPanel.SetActive(hasCards);
        }

        if (!hasCards)
            return;

        if (rewardText != null)
        {
            rewardText.text =
                $"SACRIFICE {cardCount} CARDS\n" +
                $"GAIN {cardCount} FATE";
        }

        if (confirmButton != null)
        {
            confirmButton.interactable = true;
        }

        if (cancelButton != null)
        {
            cancelButton.interactable = true;
        }
    }

    public override void UpdateVisuals()
    {
        if (isBrowsing)
            return;

        for (int i = 0; i < sacrificePoints.Count; i++)
        {
            if (i < Cards.Count)
            {
                Card card = Cards[i];

                sacrificePoints[i].Assign(
                    card,
                    occupiedPointColor
                );

                if (card != previewedCard)
                {
                    HideCardContainer(card);
                }
            }
            else
            {
                sacrificePoints[i].Clear();
            }
        }
    }

    private void ShowCardPreview(Card card)
    {
        if (card == null || cardPreviewAnchor == null)
            return;

        if (previewedCard != null && previewedCard != card)
        {
            HideCardContainer(previewedCard);
        }

        previewedCard = card;

        CardContainer container = card.Container;

        container.transform.SetParent(transform);
        container.transform.SetAsLastSibling();
        container.transform.position = cardPreviewAnchor.position;

        container.SetScale(Vector3.one * previewScale);
        container.ShowVisual(true);
        container.ShowPopUp(true);
    }

    private void HideCardPreview()
    {
        if (previewedCard == null)
            return;

        HideCardContainer(previewedCard);
        previewedCard = null;
    }

    private void HideCardContainer(Card card)
    {
        if (card == null)
            return;

        CardContainer container = card.Container;

        container.transform.SetParent(transform);

        int cardIndex = Cards.IndexOf(card);

        if (cardIndex >= 0 && cardIndex < sacrificePoints.Count)
        {
            container.transform.position =
                sacrificePoints[cardIndex].transform.position;
        }

        container.ShowPopUp(false);
        container.ShowVisual(false);
    }

    protected override void BeginDragContainer(CardContainer container)
    {
        ShowSacrificePrompt(false);

        previewedCard = container.Card;

        container.ShowVisual(true);
        container.ShowPopUp(false);
        container.SetDragging(true);
        container.transform.SetAsLastSibling();
    }

    protected override void EndDragContainer(
        CardContainer container,
        PointerEventData eventData)
    {
        container.SetDragging(false);
        previewedCard = null;

        if (Contains(eventData.position))
        {
            UpdateVisuals();
            UpdateSacrificePrompt();
            return;
        }

        GameManager.Actions.AddAction(
            new ChangeZone(container.Card, handArea)
        );
    }

    private void ConfirmSacrifice()
    {
        if (Cards.Count == 0)
            return;

        previewedCard = null;
        ShowSacrificePrompt(false);

        GameManager.Actions.AddAction(
            new SacrificeCards(
                new List<Card>(Cards)
            )
        );
    }

    private void CancelSacrifice()
    {
        ReturnAllCardsToHand();
    }

    public void ReturnAllCardsToHand()
    {
        if (Cards.Count == 0)
            return;

        HideCardPreview();
        ShowSacrificePrompt(false);

        GameManager.Actions.AddAction(
            new CancelSacrifice(
                new List<Card>(Cards),
                handArea
            )
        );
    }
}