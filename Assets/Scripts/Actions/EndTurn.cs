using System.Collections;
using System.Collections.Generic;

public class EndTurn : IAction
{
    public EndTurn() {} 

    public IEnumerator Execute()
    {
        PlayArea playArea = GameManager.Instance.PlayArea;
        Hand hand = GameManager.Instance.Hand;

        if (IsValidHand(playArea.Cards))
        {
            while (playArea.Cards.Count > 0)
            {
                Card card = playArea.Cards[0];

                yield return GameManager.Actions.ExecuteImmediate(new ChangeZone(card, GameManager.Instance.DiscardPile));

                if (card is MinorArcana minorArcana)
                {
                    yield return GameManager.Actions.ExecuteImmediate(new GainFate(minorArcana.Number));
                }
            }

            GameManager.Instance.DecrementTurn();

            while (hand.Cards.Count < GameManager.Instance.HandSize)
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
