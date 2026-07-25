using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CancelSacrifice : IAction
{
    public List<Card> Cards { get; private set; }
    public Zone HandArea { get; private set; }

    public CancelSacrifice(
        List<Card> cards,
        Zone handArea)
    {
        Cards = cards;
        HandArea = handArea;
    }

    public IEnumerator Execute()
    {
        foreach (Card card in Cards)
        {
            yield return GameManager.Actions.ExecuteImmediate(
                new ChangeZone(card, HandArea, 0f)
            );
        }

        yield return new WaitForSeconds(0.25f);
    }
}