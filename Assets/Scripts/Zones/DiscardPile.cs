using System;
using UnityEngine;

public class DiscardPile : Zone
{
    private readonly Vector3 hoverScale = new(1.2f, 1.2f, 1f);

    public Action<Card> OnClickCardInDiscardPile;

    protected override void ClickCard(Card card)
    {
        if (UIManager.Instance.BrowserOpen)
        {
            OnClickCardInDiscardPile?.Invoke(card);
        } else
        {
            UIManager.Instance.OpenBrowser(this);
        }
    }

    protected override void EnterContainer(CardContainer container)
    {
        container.SetScale(hoverScale);
        container.ShowPopUp(true);
    }

    protected override void ExitContainer(CardContainer container)
    {
        container.SetScale(Vector3.one);
        container.ShowPopUp(false);
    }
}