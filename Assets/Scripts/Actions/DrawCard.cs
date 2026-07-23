using System.Collections;
using UnityEngine;

public class DrawCard : IAction
{
    public DrawCard()
    {
    }

    public IEnumerator Execute()
    {
        Deck deck = GameManager.Instance.Deck;

        if (deck.Cards.Count <= 0) yield break;

        HandArea hand = GameManager.Instance.Hand;
        Card card = deck.Cards[0];

        //This is where the SOUND IS
        SoundFXManager.Instance.PlayDrawSoundClip(GameManager.Instance.transform);

      yield return GameManager.Actions.ExecuteImmediate(new ChangeZone(card, hand));

        if (card is Death)
        {
            Debug.Log("Game over");
        }
    }
}

public class DrawTarotCard : IAction
{
    public DrawTarotCard()
    {
    }

    public IEnumerator Execute()
    {
        Deck deck = GameManager.Instance.TarotDeck;

        if (deck.Cards.Count <= 0) yield break;

        TarotHandArea hand = GameManager.Instance.TarotHand;
        Card card = deck.Cards[0];
        
        yield return GameManager.Actions.ExecuteImmediate(new ChangeZone(card, hand));
    }
}
