using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Judgement : MajorArcana
{
    public Judgement(CardSO cardSO) : base(cardSO)
    {
        Name = "Judgement";
        FateCost = 8;
        Text = "Set Doom to 0.";
    }

    public override IEnumerator ExecuteEffect()
    {
        GameManager.Instance.SetDoom(0);

        yield return new WaitForSeconds(0.25f);
    }
}