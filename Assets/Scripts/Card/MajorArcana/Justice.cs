using System.Collections;
using UnityEngine;


public class Justice : MajorArcana
{
    public Justice(CardSO cardSO) : base(cardSO)
    {
        Name = "Justice";
        FateCost = 2;
        Text = "Reduce Doom by 1.";
    }

    public override IEnumerator ExecuteEffect()
    {
        GameManager.Instance.GainDoom(-1);

        yield return new WaitForSeconds(0.25f);
    }
}