using System;
using System.ComponentModel;
using PrimeTween;
using UnityEngine;
using UnityEngine.EventSystems;

public class DiscardPile : Zone, IPointerEnterHandler, IPointerExitHandler
{
    private readonly Vector3 hoverScale = new(1.2f, 1.2f, 1f);

    [SerializeField] private GameObject popUp;

    public Action<Card> OnClickCardInDiscardPile;

    protected override void Start()
    {
        base.Start();
        ShowPopUp(false);
    }

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

    public override void UpdateVisuals()
    {
        base.UpdateVisuals();
        foreach(Card card in Cards)
        {
            card.Container.SetScale(Vector3.one);
        }
    }

    protected override void EnterContainer(CardContainer container)
    {
        if (isBrowsing)
        {
            container.SetScale(hoverScale);
            container.ShowPopUp(true);
        } else
        {
            ShowPopUp(true);
        }
    }

    protected override void ExitContainer(CardContainer container)
    {
        container.SetScale(Vector3.one);
        container.ShowPopUp(false);

        ShowPopUp(false);
    }

    private void ShowPopUp(bool enabled)
    {
        if (popUp == null) return;

        Tween.Scale(
            popUp.transform,
            enabled ? Vector3.one : Vector3.zero,
            0.1f
        );
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ShowPopUp(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ShowPopUp(false);
    }

    
}