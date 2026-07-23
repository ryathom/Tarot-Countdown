using System.Collections;

public class World : MajorArcana
{
    public World(CardSO cardSO) : base(cardSO)
    {
        Name = "The World";
        FateCost = 4;
    }

    public override IEnumerator ExecuteEffect()
    {
        yield return ShuffleBackSuit(Suit.Swords);
    }
}

