using System;
using System.Collections.Generic;
using UnityEngine;

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
        card.SetZone(this);
        Cards.Add(card);
        OnContentsChange?.Invoke();
    }

    public virtual void RemoveCard(Card card)
    {
        Cards.Remove(card);
        OnContentsChange?.Invoke();
    }
}
