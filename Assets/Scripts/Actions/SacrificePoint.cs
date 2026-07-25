using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SacrificePoint : MonoBehaviour,
    IPointerEnterHandler,
    IPointerExitHandler
{
    [SerializeField] private Image pointImage;

    public Card AssignedCard { get; private set; }

    public Action<Card> HoverEntered;
    public Action HoverExited;

    public void Assign(Card card, Color colour)
    {
        AssignedCard = card;

        pointImage.color = colour;
        pointImage.enabled = true;
    }

    public void Clear()
    {
        AssignedCard = null;
        pointImage.enabled = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (AssignedCard != null)
            HoverEntered?.Invoke(AssignedCard);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        HoverExited?.Invoke();
    }
}