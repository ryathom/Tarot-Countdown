using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTurn : IAction
{
    public EndTurn() {} 

    public IEnumerator Execute()
    {
        PlayArea playArea = GameManager.Instance.PlayArea;
        HandArea handArea = GameManager.Instance.Hand;

        if (IsValidHand(playArea.Cards))
        {
            Hand hand = HandRanker.GetHand(playArea.Cards);

            Debug.Log(hand.GetType().Name);
            Debug.Log(hand.GetScore());

            while (playArea.Cards.Count > 0)
            {
                Card card = playArea.Cards[0];

                yield return GameManager.Actions.ExecuteImmediate(new ChangeZone(card, GameManager.Instance.DiscardPile));
            }

            GameManager.Instance.DecrementTurn();

            while (handArea.Cards.Count < GameManager.Instance.HandSize)
            {
                yield return GameManager.Actions.ExecuteImmediate(new DrawCard());
            }
        } else
        {
            yield break;
        }
    }

    public bool IsValidHand(List<Card> cards)
    {
        if (cards.Count == 0) return false;

        return true;
    }
}
