using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SacrificeCards : IAction
{
    public List<Card> Cards { get; private set; }

    public SacrificeCards(List<Card> cards)
    {
        Cards = cards;
    }

    public IEnumerator Execute()
    {
        int cardsSacrificed = Cards.Count;

        if (cardsSacrificed == 0)
        {
            yield break;
        }

        GameManager.Instance.CanSacrifice = false;

        GameManager.Instance.SacrificeArea
            ?.UpdateSacrificeAvailability();

        foreach (Card card in Cards)
        {
            Zone currentZone = card.Zone;

            currentZone.RemoveCard(card);

            card.Container.gameObject.SetActive(false);
        }

        yield return GameManager.Actions.ExecuteImmediate(
            new GainFate(cardsSacrificed)
        );

        for (int i = 0; i < cardsSacrificed; i++)
        {
            yield return GameManager.Actions.ExecuteImmediate(
                new DrawCard()
            );
        }

        yield return new WaitForSeconds(0.25f);
    }
}