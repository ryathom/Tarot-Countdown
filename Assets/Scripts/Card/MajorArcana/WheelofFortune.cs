using System.Collections;
using UnityEngine;


public class WheelofFortune : MajorArcana
{
    public WheelofFortune(CardSO cardSO) : base(cardSO)
    {
        Name = "Wheel of Fortune";
        FateCost = 5;
        Text = "Transform each card in your hand into a card with a random suit and number.";
    }

    public override IEnumerator ExecuteEffect()
    {
        foreach(Card card in GameManager.Instance.Hand.Cards)
        {
            yield return new WaitForSeconds(0.2f);

            card.Number = Random.Range(1,15);
            card.Suit = (Suit)Random.Range(0,4);
            card.Container.ShowVisual(true);
        }

        yield return new WaitForSeconds(0.2f);
        GameManager.Instance.Hand.UpdateVisuals();
    }
}