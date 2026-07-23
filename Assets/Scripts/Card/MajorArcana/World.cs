using System.Collections;

public class World : MajorArcana
{
    public World(CardSO cardSO) : base(cardSO)
    {
        Name = "The World";
        FateCost = 4;
        Text = "Shuffle all Swords from your discard pile back into your deck.";
    }

    public override IEnumerator ExecuteEffect()
    {
        yield return ShuffleBackSuit(Suit.Swords);
    }
}

