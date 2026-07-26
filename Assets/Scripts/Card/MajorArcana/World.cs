using System.Collections;

public class World : MajorArcana
{
    public World(CardSO cardSO) : base(cardSO)
    {
        Name = "The World";
        FateCost = 2;
        Text = "Transform all cards in your hand into Swords.";
    }

    public override IEnumerator ExecuteEffect()
    {
        yield return TransformHandIntoSuit(Suit.Swords);
    }
}

