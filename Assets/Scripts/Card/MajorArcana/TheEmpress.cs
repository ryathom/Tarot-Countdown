using System.Collections;
using UnityEngine;


public class TheEmpress : MajorArcana
{
    public TheEmpress(CardSO cardSO) : base(cardSO)
    {
        Name = "The Empress";
        FateCost = 3;
        Text = "Create a new 3 of each suit and place them in random positions in the deck.";
    }

    public override IEnumerator ExecuteEffect()
    {
        SoundFXManager.Instance.PlayempressSoundClip(GameManager.Instance.transform);

        for (int i = 0; i < 4; i++)
        {
            Card card = GameManager.Instance.InstantiateMinorArcana(3, (Suit)i);
            GameManager.Instance.Deck.InsertCard(card, Random.Range(0, GameManager.Instance.Deck.Cards.Count));

            yield return new WaitForSeconds(0.25f);
        }
    }
}