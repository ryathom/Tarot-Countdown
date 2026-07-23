using System.Collections;

public class Star : MajorArcana
{
    public Star(CardSO cardSO) : base(cardSO)
    {
        Name = "The Star";
        FateCost = 4;
    }

    public override IEnumerator ExecuteEffect()
    {
        yield return ShuffleBackSuit(Suit.Coins);
    }
}

