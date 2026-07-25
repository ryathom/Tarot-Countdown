using System.Collections;
using UnityEngine;


public class Magician : MajorArcana
{
    public Magician(CardSO cardSO) : base(cardSO)
    {
        Name = "The Magician";
        FateCost = 1;
        Text = "Transform a random card in your hand into an Ace.";
    }

    public override IEnumerator ExecuteEffect()
    {
        yield return new WaitForSeconds(0.25f);

        int idx = Random.Range(0, GameManager.Instance.Hand.Cards.Count);
        Card card = GameManager.Instance.Hand.Cards[idx];

        card.Container.SetScale(new (1.2f, 1.2f, 1f));

        yield return new WaitForSeconds(0.25f);

        card.Number = 1;
        card.Container.ShowVisual(true);

        yield return new WaitForSeconds(0.25f);

        card.Container.SetScale(new (1f, 1f, 1f));
        GameManager.Instance.Hand.UpdateVisuals();

        yield return new WaitForSeconds(0.25f);
    }
}