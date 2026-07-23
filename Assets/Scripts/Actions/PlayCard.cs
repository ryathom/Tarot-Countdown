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
    }
}

public class UnplayCard : IAction
{
    public Card Card {get; private set;}

    public UnplayCard(Card card)
    {
        Card = card;
    }

    public IEnumerator Execute()
    {
        HandArea hand = GameManager.Instance.Hand;

        yield return GameManager.Actions.ExecuteImmediate(new ChangeZone(Card, hand));
    }
}
