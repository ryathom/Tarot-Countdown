using UnityEngine;

public class CardContainer : MonoBehaviour
{
    [SerializeField] private CardVisual cardVisual;

    public Card Card {get; private set;}

    public void SetCard(Card card)
    {
        Card = card;
        Card.SetContainer(this);

        cardVisual.SetCard(card);
    }
}
