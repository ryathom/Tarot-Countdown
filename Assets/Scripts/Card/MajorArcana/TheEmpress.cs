using System.Collections;
using UnityEngine;


public class TheEmpress : MajorArcana
{
    public TheEmpress(CardSO cardSO) : base(cardSO)
    {
        Name = "The Empress";
        FateCost = 3;
        Text = "Create a new random card of each suit and shuffle them into the deck.";
    }

    public override IEnumerator ExecuteEffect()
    {
        int x;

        for (int i = 0; i < 4; i++)
        {
            x = Random.Range(1,15);
            Card card = GameManager.Instance.InstantiateMinorArcana(x, (Suit)i);

            card.Container.transform.SetAsLastSibling();
            card.SetFaceUp(true);
            yield return new WaitForSeconds(0.75f);

            GameManager.Instance.Deck.InsertCard(card, Random.Range(0, GameManager.Instance.Deck.Cards.Count));

            GameManager.Instance.Deck.Shuffle();

            yield return new WaitForSeconds(0.25f);
        }
    }
}