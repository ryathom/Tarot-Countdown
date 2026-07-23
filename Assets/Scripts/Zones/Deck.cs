using UnityEngine;

public class Deck : Zone
{
    public override void AddCard(Card card)
    {
        base.AddCard(card);
        card.SetFaceUp(false);
    }

    protected override void ClickCard(Card card)
    {
        GameManager.Actions.AddAction(new DrawCard());
    }

    public override void InsertCard(Card card, int position)
    {
        base.InsertCard(card, position);
        card.SetFaceUp(false);
    }

    public void Shuffle()
    {
        for (int i = 0; i < Cards.Count; i++) 
        {
            Card temp = Cards[i];
            int randomIndex = Random.Range(i, Cards.Count);
            Cards[i] = Cards[randomIndex];
            Cards[randomIndex] = temp;
        }

        UpdateVisuals();
    }

    public int DeathCardPosition()
    {
        foreach(Card card in Cards)
        {
            if (card is Death)
            {
                return Cards.IndexOf(card);
            }
        }

        return -1;
    }
}