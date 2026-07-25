using System.Collections;

public class Star : MajorArcana
{
    public Star(CardSO cardSO) : base(cardSO)
    {
        Name = "The Star";
        FateCost = 2;
        Text = "Transform all cards in your hand into Pentacles.";
    }

    public override IEnumerator ExecuteEffect()
    {
        yield return TransformHandIntoSuit(Suit.Pentacles);
    }
}

