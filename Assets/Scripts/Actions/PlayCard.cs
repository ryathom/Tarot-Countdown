using System.Collections;

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

        if (Card is MinorArcana minorArcana)
        {
            GameManager.Actions.AddAction(new MillCards(minorArcana.Number));
        }
    }
}
