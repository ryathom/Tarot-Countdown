using System.Collections;

public class Sun : MajorArcana
{
    public Sun(CardSO cardSO) : base(cardSO)
    {
        Name = "The Sun";
        FateCost = 4;
        Text = "Transform all cards in your hand into Cups.";
    }

    public override IEnumerator ExecuteEffect()
    {
        yield return TransformHandIntoSuit(Suit.Cups);
    }
}

