using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class HandArea : Zone
{
    [SerializeField] private float cardSpacing = 180;
    private int yScale = 0;

    private readonly Vector3 hoverScale = new(1.2f, 1.2f, 1f);

    private readonly float minDragThreshold = 300f;
    private readonly float maxDragThreshold = 700f;

    public Action<Card> OnClickCardInHand;

    // Methods
    //---------------------------------------------------------------------------------------------------------
    public override void UpdateVisuals()
    {
        for (int i = 0; i < Cards.Count; i++)
        {
            float relativePosition = i - ((Cards.Count - 1f) / 2f);
            
            float x = relativePosition * cardSpacing;

            float y = -1 - (relativePosition * relativePosition / (Cards.Count * 2));
            y *= yScale;


            Vector2 targetPosition = new(x, y);

            Cards[i].Container.transform.SetAsLastSibling();
            Cards[i].Container.transform.SetParent(this.transform);
            Cards[i].Container.SetTargetPosition(this.transform.position + (Vector3)targetPosition);
            Cards[i].Container.ShowVisual(true);
        }
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
        Debug.Log(eventData.position);
        
        if (eventData.position.y > minDragThreshold && eventData.position.y < maxDragThreshold)
        {
            GameManager.Actions.AddAction(new PlayCard(container.Card));
        } else
        {
            UpdateVisuals();
        }
    }
}