using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardBrowser : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    // private readonly float cardSpacing = 40;

    private Zone currentZone;

    public void Open(Zone zone)
    {
        zone.isBrowsing = true;
        currentZone = zone;
        List<Card> Cards = zone.Cards;

        float cardSpacing = 1800 / Cards.Count;
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
        currentZone.isBrowsing = false;
        currentZone.UpdateVisuals();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        UIManager.Instance.CloseBrowser();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
    }

    public void OnPointerExit(PointerEventData eventData)
    {
    }
}