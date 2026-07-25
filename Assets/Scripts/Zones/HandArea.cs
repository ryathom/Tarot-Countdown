using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class HandArea : Zone
{
    [SerializeField] private bool TarotHand;

    private float cardSpacing;

    private readonly Vector3 hoverScale = new(1.2f, 1.2f, 1f);

    private float minDragThreshold;
    private float maxDragThreshold;
    private float minSacrificeThreshold;

    public Action<Card> OnClickCardInHand;

    // Methods
    //---------------------------------------------------------------------------------------------------------
   protected override void Start()
    {
        base.Start();

        cardSpacing = 180f * Screen.width / 1920f;
        minDragThreshold = 300f * Screen.height / 1080f;
        maxDragThreshold = 700f * Screen.height / 1080f;
        minSacrificeThreshold = 300f * Screen.width / 1920f;
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

            Cards[i].Container.transform.SetAsLastSibling();
            Cards[i].Container.transform.SetParent(this.transform);
            Cards[i].Container.SetTargetPosition(this.transform.position + (Vector3)targetPosition);
            Cards[i].Container.ShowVisual(true);
        }
    }

    public void SortHand()
    {
        Cards.Sort(delegate(Card x, Card y)
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
        } else
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
        container.SetDragging(true);
    }

    protected override void EndDragContainer(CardContainer container, PointerEventData eventData)
    {
        container.SetDragging(false);
        
        if (eventData.position.y > minDragThreshold && eventData.position.y < maxDragThreshold)
        {
            GameManager.Actions.AddAction(new PlayCard(container.Card));
        } else if (eventData.position.x < minSacrificeThreshold && eventData.position.y < minDragThreshold)
        {
            GameManager.Actions.AddAction(new SacrificeCard(container.Card));
        }
        {
            UpdateVisuals();
        }
    }
}