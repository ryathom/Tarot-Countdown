using System.Collections.Generic;
using Unity.Multiplayer.Center.Common;
using UnityEngine;
using UnityEngine.UI;

public class PlayArea : Zone
{
    private float cardSpacing;
    private readonly Vector3 hoverScale = new(1.1f, 1.1f, 1f);

    [SerializeField] private List<Image> placementZones;
    [SerializeField] private Color selectColor;
    [SerializeField] private Color deselectColor;

    public bool isScoring;

    // Methods
    //---------------------------------------------------------------------------------------------------------
    protected override void Start()
    {
        base.Start();
        SetPlacementZoneColors();
    }

    public override void AddCard(Card card)
    {
        base.AddCard(card);

        SoundFXManager.Instance.PlayCardSoundClip(GameManager.Instance.transform);

    }

    public override void UpdateVisuals()
    {
        if (isScoring) return;

        for (int i = 0; i < Cards.Count; i++)
        {
            Cards[i].Container.transform.SetAsLastSibling();
            Cards[i].Container.transform.SetParent(this.transform);
            Cards[i].Container.SetTargetPosition(placementZones[i].transform.position);
            Cards[i].Container.ShowVisual(true);
            Cards[i].Container.ShowPopUp(false);
            Cards[i].Container.SetScale(Vector3.one);
        }

        SetPlacementZoneColors();
    }

    public void SetPlacementZoneColors()
    {
        for (int i = 0; i < 6; i++)
        {
            if (i == Cards.Count)
            {
                placementZones[i].color = selectColor;
            } else
            {
                placementZones[i].color = deselectColor;
            }
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
