using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardBrowser : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    private float cardSpacing = 40;

    private Zone currentZone;
    private bool canClose;

    public bool isOpen = false;

    public void Open(Zone zone, bool canClose = true)
    {
        this.canClose = canClose;
        zone.isBrowsing = true;
        isOpen = true;
        currentZone = zone;
        List<Card> Cards = zone.Cards;

        if (Cards.Count > 0)
        {
            cardSpacing = 1800 / Cards.Count;
        } else
        {
            cardSpacing = 40;
        }
        cardSpacing = Mathf.Clamp(cardSpacing, 10, 200);

        for (int i = Cards.Count - 1; i >= 0; i--)
        {
            float relativePosition = i - ((Cards.Count - 1f) / 2f);
            
            float x = relativePosition * cardSpacing;

            float y = -1 - (relativePosition * relativePosition / (Cards.Count * 2));


            Vector2 targetPosition = new(x, y);

            Cards[i].Container.transform.SetParent(this.transform);
            Cards[i].Container.transform.SetAsLastSibling();
            Cards[i].Container.SetTargetPosition(this.transform.position + (Vector3)targetPosition);
            Cards[i].Container.ShowVisual(true);
        }
    }

    public void Close()
    {
        isOpen = false;
        currentZone.isBrowsing = false;
        currentZone.UpdateVisuals();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (canClose) UIManager.Instance.CloseBrowser();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
    }

    public void OnPointerExit(PointerEventData eventData)
    {
    }
}