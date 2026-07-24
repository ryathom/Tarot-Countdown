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
            GameManager.Actions.AddAction(new GameOver(false));
        }
    }
}

public class GameOver : IAction
{
    public bool Victory;

    public GameOver(bool victory)
    {
        Victory = victory;
    }

    public IEnumerator Execute()
    {
        if (Victory)
        {
            UIManager.Instance.ShowGameOverScreen("You win!");
        } else
        {
            UIManager.Instance.ShowGameOverScreen("You died.");
        }

        return null;
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

        if (deck.Cards.Count <= 0)
        {
            yield return RecycleTarot();
        }

        HandArea hand = GameManager.Instance.TarotHand;
        Card card = deck.Cards[0];
        
        yield return GameManager.Actions.ExecuteImmediate(new ChangeZone(card, hand));
    }

    public IEnumerator RecycleTarot()
    {
        Deck deck = GameManager.Instance.TarotDeck;
        DiscardPile discardPile = GameManager.Instance.TarotDiscardPile;

        while (discardPile.Cards.Count > 0)
        {
            yield return GameManager.Actions.ExecuteImmediate(new ChangeZone(discardPile.Cards[0], deck, 0.1f));
        }

        yield return new WaitForSeconds(0.25f);

        deck.Shuffle();
    }
}
