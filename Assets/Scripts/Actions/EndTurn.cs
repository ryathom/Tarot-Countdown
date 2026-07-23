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

        while (handArea.Cards.Count < GameManager.Instance.HandSize)
        {
            yield return GameManager.Actions.ExecuteImmediate(new DrawCard());
        }

        if (!IsValidRun(playArea.Cards))
        {
            int score = ScoreRun(playArea.Cards);
            yield return GameManager.Actions.ExecuteImmediate(new GainFate(score));

            while (!IsValidRun(playArea.Cards))
            {
                yield return GameManager.Actions.ExecuteImmediate(new ChangeZone(playArea.Cards[0], GameManager.Instance.DiscardPile));
            }
        } else if (!HasValidPlays(playArea.Cards, handArea.Cards))
        {
            int score = ScoreRun(playArea.Cards);
            yield return GameManager.Actions.ExecuteImmediate(new GainFate(score));

            while (playArea.Cards.Count > 0)
            {
                yield return GameManager.Actions.ExecuteImmediate(new ChangeZone(playArea.Cards[0], GameManager.Instance.DiscardPile));
            }
        }

        

        GameManager.Instance.DecrementTurn();
    }

    public int ScoreRun(List<Card> playArea)
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
