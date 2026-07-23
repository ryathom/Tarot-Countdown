using System.Collections;
using UnityEngine;

public class PlayCard : IAction
{
    public Card Card {get; private set;}

    public PlayCard(Card card)
    {
        Card = card;
    }

    public IEnumerator Execute()
    {
        PlayArea playArea = GameManager.Instance.PlayArea;

        yield return GameManager.Actions.ExecuteImmediate(new ChangeZone(Card, playArea));

        if (GetMillCost() >= 0)
        {
            yield return GameManager.Actions.ExecuteImmediate(new MillCards(GetMillCost()));
        }

        GameManager.Actions.AddAction(new EndTurn());
    }

    public int GetMillCost()
    {
        if (Card.Number >= 9) return 3;

        if (Card.Number >= 6) return 2;

        if (Card.Number >= 3) return 1;
        
        return 0;
    }
}
