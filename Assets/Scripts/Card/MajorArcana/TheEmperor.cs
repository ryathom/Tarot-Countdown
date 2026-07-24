using System.Collections;
using UnityEngine;


public class TheEmperor : MajorArcana
{
    public TheEmperor(CardSO cardSO) : base(cardSO)
    {
        Name = "The Emperor";
        FateCost = 10;
        Text = "Skip a number of turns equal to your Doom";
    }

    public override IEnumerator ExecuteEffect()
    {
        yield return new WaitForSeconds(0.5f);

        for (int i = 0; i < GameManager.Instance.Doom - 1; i++)
        {
            GameManager.Instance.DecrementTurn();
            yield return new WaitForSeconds(0.25f);
        }

        yield return new WaitForSeconds(0.25f);
    }
}