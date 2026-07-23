using System.Collections;

public class Sun : MajorArcana
{
    public Sun(CardSO cardSO) : base(cardSO)
    {
        Name = "The Sun";
        FateCost = 4;
    }

    public override IEnumerator ExecuteEffect()
    {
        yield return ShuffleBackSuit(Suit.Cups);
    }
}

