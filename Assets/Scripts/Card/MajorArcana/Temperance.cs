using System.Collections;
using UnityEngine;


public class Temperance : MajorArcana
{
    public Temperance(CardSO cardSO) : base(cardSO)
    {
        Name = "Temperance";
        FateCost = 5;
        Text = "Reduce Doom by 2.";
    }

    public override IEnumerator ExecuteEffect()
    {
        GameManager.Instance.GainDoom(-2);

        yield return new WaitForSeconds(0.25f);
    }
}