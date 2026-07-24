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
        if (Card is MinorArcana)
        {
            yield return PlayMinorArcana();
        } else if (Card is MajorArcana majorArcana)
        {
            yield return PlayMajorArcana(majorArcana);
        }
    }

    public IEnumerator PlayMinorArcana()
    {
        PlayArea playArea = GameManager.Instance.PlayArea;

        if (playArea.Cards.Count == 0 && Card.Number == 1)
        {
            Card.EffectiveNumber = 15;
        } else
        {
            Card.EffectiveNumber = Card.Number;
        }

        yield return GameManager.Actions.ExecuteImmediate(new ChangeZone(Card, playArea));

        if (Card.GetMillCost() >= 0)
        {
            yield return GameManager.Actions.ExecuteImmediate(new MillCards(Card.GetMillCost()));
        }

        GameManager.Actions.AddAction(new EndTurn());
    }

    public IEnumerator PlayMajorArcana(MajorArcana arcana)
    {
        if (arcana.FateCost > GameManager.Instance.Fate) yield break;

        yield return GameManager.Actions.ExecuteImmediate(new GainFate(-arcana.FateCost));

        yield return arcana.ExecuteEffect();

        yield return GameManager.Actions.ExecuteImmediate(new ChangeZone(arcana, GameManager.Instance.TarotDiscardPile));

        GameManager.Actions.AddAction(new EndTurn());
    }
}
