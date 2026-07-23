using System.Collections;

public class Moon : MajorArcana
{
    public Moon(CardSO cardSO) : base(cardSO)
    {
        Name = "The Moon";
        FateCost = 4;
    }

    public override IEnumerator ExecuteEffect()
    {
        yield return ShuffleBackSuit(Suit.Wands);
    }
}

