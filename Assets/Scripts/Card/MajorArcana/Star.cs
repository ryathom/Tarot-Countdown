using System.Collections;

public class Star : MajorArcana
{
    public Star(CardSO cardSO) : base(cardSO)
    {
        Name = "The Star";
        FateCost = 4;
        Text = "Shuffle all Coins from your discard pile back into your deck.";
    }

    public override IEnumerator ExecuteEffect()
    {
        yield return ShuffleBackSuit(Suit.Coins);
    }
}

