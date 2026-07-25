using System;
using PrimeTween;
using UnityEngine;
using UnityEngine.EventSystems;

public class HandArea : Zone
{
    [SerializeField] private bool TarotHand;

    private float cardSpacing;

    private readonly Vector3 hoverScale = new(1.2f, 1.2f, 1f);

    public Action<Card> OnClickCardInHand;

    public bool IsDraggingCard { get; private set; }

    // Methods
    //---------------------------------------------------------------------------------------------------------
    protected override void Start()
    {
        base.Start();

        cardSpacing = 180f * Screen.width / 1920f;
    }

    public override void UpdateVisuals()
    {
        SortHand();

        for (int i = 0; i < Cards.Count; i++)
        {
            float relativePosition = i - ((Cards.Count - 1f) / 2f);

            float x = relativePosition * cardSpacing;

            if (TarotHand) x *= -1;

            Vector2 targetPosition = new(x, 0);

            CardContainer container = Cards[i].Container;

            container.transform.SetAsLastSibling();
            container.transform.SetParent(transform);

            // Reset any scale applied while previewing the sacrifice.
            container.SetScale(Vector3.one);

            container.SetTargetPosition(
                transform.position + (Vector3)targetPosition
            );

            container.ShowVisual(true);
        }
    }

    public void SortHand()
    {
        Cards.Sort(delegate (Card x, Card y)
        {
            if (x.Number == y.Number) return 0;
            else if (x.Number > y.Number) return -1;
            else return 1;
        });
    }

    // Gameplay
    //---------------------------------------------------------------------------------------------------------
    protected override void ClickCard(Card card)
    {
        if (isBrowsing)
        {
            OnClickCardInHand?.Invoke(card);
        }
        else
        {
            GameManager.Actions.AddAction(new PlayCard(card));
        }
    }

    protected override void EnterContainer(CardContainer container)
    {
        if (!GameManager.Instance.InputEnabled) return;
        if (container.IsDragging) return;

        container.SetScale(hoverScale);
        container.ShowPopUp(true);
        SoundFXManager.Instance.PlayHoverSoundClip(GameManager.Instance.transform);
    }

    protected override void ExitContainer(CardContainer container)
    {
        if (container.IsDragging) return;

        container.SetScale(Vector3.one);
        container.ShowPopUp(false);

    }

    protected override void BeginDragContainer(CardContainer container)
    {
        IsDraggingCard = true;
        container.SetDragging(true);
    }

    protected override void EndDragContainer(
        CardContainer container,
        PointerEventData eventData)
    {
        IsDraggingCard = false;
        container.SetDragging(false);

        if (GameManager.Instance.PlayArea.Contains(eventData.position))
        {
            GameManager.Actions.AddAction(new PlayCard(container.Card));
        }

        if (GameManager.Instance.SacrificeArea.Contains(eventData.position))
        {
            if (GameManager.Instance.SacrificeArea.CanAddCard(container.Card))
            {
                GameManager.Actions.AddAction(
                    new ChangeZone(container.Card, GameManager.Instance.SacrificeArea)
                );
            }
            else
            {
                UpdateVisuals();
            }

            return;
        }

        UpdateVisuals();
    }
}