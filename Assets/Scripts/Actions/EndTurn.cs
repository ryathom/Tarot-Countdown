using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTurn : IAction
{
    public EndTurn() {}

    private readonly int minimumRunSize = 3;

    public IEnumerator Execute()
    {
        PlayArea playArea = GameManager.Instance.PlayArea;
        HandArea handArea = GameManager.Instance.Hand;
        Deck deck = GameManager.Instance.Deck;
        HandArea tarotHand = GameManager.Instance.TarotHand;
        Deck tarotDeck = GameManager.Instance.TarotDeck;

        while (handArea.Cards.Count < GameManager.Instance.HandSize && deck.Cards.Count > 0)
        {
            yield return GameManager.Actions.ExecuteImmediate(new DrawCard());
        }

        while (tarotHand.Cards.Count < GameManager.Instance.TarotHandSize && tarotDeck.Cards.Count > 0)
        {
            yield return GameManager.Actions.ExecuteImmediate(new DrawTarotCard());
        }

        if (!IsValidRun(playArea.Cards))
        {
            yield return ScoreRun(playArea);

            while (!IsValidRun(playArea.Cards))
            {
                yield return GameManager.Actions.ExecuteImmediate(new ChangeZone(playArea.Cards[0], GameManager.Instance.DiscardPile));
            }
        } else if (!HasValidPlays(playArea.Cards, handArea.Cards))
        {
            yield return ScoreRun(playArea);

            while (playArea.Cards.Count > 0)
            {
                yield return GameManager.Actions.ExecuteImmediate(new ChangeZone(playArea.Cards[0], GameManager.Instance.DiscardPile));
            }
        }

        

        GameManager.Instance.DecrementTurn();
    }

    public IEnumerator ScoreRun(PlayArea playArea)
    {
        int score = CalculateScore(playArea.Cards);
        yield return GameManager.Actions.ExecuteImmediate(new GainFate(score));
        yield return GameManager.Actions.ExecuteImmediate(new GainDoom(1));
    }

    public int CalculateScore(List<Card> playArea)
    {
        List<Card> currentRun = new();
        int score = 0;

        foreach (Card card in playArea)
        {
            if (currentRun.Count == 0)
            {
                currentRun.Add(card);
            } else if (card.Suit == currentRun[^1].Suit)
            {
                currentRun.Add(card);
            } else
            {   
                currentRun.Clear();
                currentRun.Add(card);
            }

            if (currentRun.Count >= minimumRunSize)
            {
                int currentScore = 0;
                foreach(Card c in currentRun)
                {
                    currentScore += c.Number;
                }

                score = currentScore > score ? currentScore : score;
            }
        }

        return score;
    }

    public bool HasValidPlays(List<Card> playArea, List<Card> handArea)
    {
        if (playArea.Count == 0) return true;

        Card lastPlayedCard = playArea[^1];

        foreach(Card card in handArea)
        {
            if (card.Number < lastPlayedCard.Number)
            {
                return true;
            }
        }

        return false;
    }

    public bool IsValidRun(List<Card> cards)
    {
        int num = int.MaxValue;

        foreach(Card card in cards)
        {
            if (card.Number < num)
            {
                num = card.Number;
            } else
            {
                return false;
            }
        }

        return true;
    }
}
