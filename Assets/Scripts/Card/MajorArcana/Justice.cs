using System.Collections;
using UnityEngine;


public class Justice : MajorArcana
{
    public Justice(CardSO cardSO) : base(cardSO)
    {
        Name = "Justice";
        FateCost = 4;
        Text = "If you have 0 Doom, reduce the turn count by 1.";
    }

    public override IEnumerator ExecuteEffect()
    {
        if (GameManager.Instance.Doom == 0)
        {
            GameManager.Instance.DecrementTurn();
        }

        return null;
    }
}