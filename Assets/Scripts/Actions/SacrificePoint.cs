using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SacrificePoint : MonoBehaviour,
    IPointerEnterHandler,
    IPointerExitHandler
{
    [SerializeField] private Image pointImage;
    [SerializeField] private GameObject flame;

    public Card AssignedCard { get; private set; }

    public Action<Card> HoverEntered;
    public Action HoverExited;

    private void Awake()
    {
        flame.SetActive(false);
    }

    public void Assign(Card card, Color colour)
    {
        AssignedCard = card;

        pointImage.color = colour;
        pointImage.enabled = true;

        flame.SetActive(true);
    }

    public void Clear()
    {
        AssignedCard = null;

        pointImage.enabled = false;
        flame.SetActive(false);
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