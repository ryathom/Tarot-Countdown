using System.Collections;

public abstract class MajorArcana : Card
{
    public MajorArcana(CardSO cardSO) : base(cardSO)
    {
    }

    public int FateCost;

    public abstract IEnumerator ExecuteEffect();
}