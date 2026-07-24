using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTurn : IAction
{
    public EndTurn() {}

    private readonly int minimumRunSize = 6;
    private readonly int maximumRunSize = 6;
    private readonly int baseScore = 5;

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

        yield return GameManager.Actions.ExecuteImmediate(new MillCards(GameManager.Instance.Doom));

        if (!IsValidRun(playArea.Cards))
        {
            yield return GameManager.Actions.ExecuteImmediate(new GainDoom(1));

            yield return ScoreRun(playArea);

            while (!IsValidRun(playArea.Cards))
            {
                yield return GameManager.Actions.ExecuteImmediate(new ChangeZone(playArea.Cards[0], GameManager.Instance.DiscardPile));
            }

            ResetAceNumber(playArea.Cards);
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
    }

    public int CalculateScore(List<Card> playArea)
    {
        List<Card> currentRun = new();
        int score = 0;

        foreach (Card card in playArea)
        {
            // Add card to run
            currentRun.Add(card);

            // If this makes run invalid, score based on prev cards
            if (!IsValidRun(currentRun))
            {
                currentRun.Remove(card);
            }
        }

        // Base score
        if (currentRun.Count >= minimumRunSize)
        {
            score = baseScore;   
        }

        // Suit bonus
        score += CalculateSuitBonus(currentRun);

        return score;
    }

    public int CalculateSuitBonus(List<Card> cards)
    {
        List<Card> suitRun = new();

        int suitCount = 0;

        foreach (Card card in cards)
        {
            if (suitRun.Count == 0)
            {
                suitRun.Add(card);
            } else if (card.Suit == suitRun[^1].Suit)
            {
                suitRun.Add(card);
            } else
            {   
                suitCount = suitRun.Count > suitCount ? suitRun.Count : suitCount;

                suitRun.Clear();
                suitRun.Add(card);
            }
        }

        return suitCount switch
        {
            6 => 11,
            5 => 8,
            4 => 5,
            3 => 3,
            _ => 0
        };
    }

    public bool HasValidPlays(List<Card> playArea, List<Card> handArea)
    {
        if (playArea.Count == 0) return true;
        if (playArea.Count >= maximumRunSize) return false;

        Card lastPlayedCard = playArea[^1];

        foreach(Card card in handArea)
        {
            if (card.EffectiveNumber < lastPlayedCard.EffectiveNumber)
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
            if (card.EffectiveNumber < num)
            {
                num = card.EffectiveNumber;
            } else
            {
                return false;
            }
        }

        return true;
    }

    public void ResetAceNumber(List<Card> cards)
    {
        if (cards.Count != 1) return;

        if (cards[0].Number == 1) cards[0].EffectiveNumber = 15;
    }
}
