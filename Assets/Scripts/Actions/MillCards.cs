using System.Collections;

public class MillCards : IAction
{
    public int Number {get; private set;}

    public MillCards(int num)
    {
        Number = num;
    }

    public IEnumerator Execute()
    {
        DiscardPile pile = GameManager.Instance.DiscardPile;
        Deck deck = GameManager.Instance.Deck;

        for (int i = 0; i < Number; i++)
        {
            if (deck.Cards.Count <= 0) yield break;

            Card card = deck.Cards[0];
            yield return GameManager.Actions.ExecuteImmediate(new ChangeZone(card, pile));
        }
    }
}
