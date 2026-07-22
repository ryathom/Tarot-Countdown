using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Zone : MonoBehaviour
{
    public List<Card> Cards {get; protected set;}

    public Action OnContentsChange;

    public void Start()
    {
        Cards = new();
    }

    public virtual void AddCard(Card card)
    {
        DeregisterContainers();
        card.SetZone(this);
        card.SetFaceUp(true);
        Cards.Add(card);
        OnContentsChange?.Invoke();
        UpdateVisuals();
        RegisterContainers();
    }

    public virtual void RemoveCard(Card card)
    {
        DeregisterContainers();
        Cards.Remove(card);
        OnContentsChange?.Invoke();
        UpdateVisuals();
        RegisterContainers();
    }

    public virtual void UpdateVisuals()
    {
        foreach(Card card in Cards)
        {
            card.Container.SetTargetPosition(this.transform.position);
        }
    }

    public virtual void RegisterContainers()
    {
        foreach(Card card in Cards)
        {
            card.Container.OnClickCard += ClickCard;
            card.Container.OnRightClickCard += RightClickCard;
            card.Container.OnBeginDragContainer += BeginDragContainer;
            card.Container.OnEndDragContainer += EndDragContainer;
            card.Container.OnEnterContainer += EnterContainer;
            card.Container.OnExitContainer += ExitContainer;
        }
    }

    public virtual void DeregisterContainers()
    {
        foreach(Card card in Cards)
        {
            card.Container.OnClickCard -= ClickCard;
            card.Container.OnRightClickCard -= RightClickCard;
            card.Container.OnBeginDragContainer -= BeginDragContainer;
            card.Container.OnEndDragContainer -= EndDragContainer;
            card.Container.OnEnterContainer -= EnterContainer;
            card.Container.OnExitContainer -= ExitContainer;
        }
    }

    protected virtual void ClickCard(Card card) {}
    protected virtual void RightClickCard(Card card) {}
    protected virtual void BeginDragContainer(CardContainer container) {}
    protected virtual void EndDragContainer(CardContainer container, PointerEventData eventData) {}
    protected virtual void EnterContainer(CardContainer container) {}
    protected virtual void ExitContainer(CardContainer container) {}
}
