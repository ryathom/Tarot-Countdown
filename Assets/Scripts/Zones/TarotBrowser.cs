using System.Collections.Generic;
using UnityEngine;

public class TarotBrowser : Zone
{
    [SerializeField] private CardContainer cardPrefab;
    [SerializeField] private MajorArcanaSO majorArcanaSO;
    [SerializeField] private Transform cardGrid;

    private readonly Vector3 hoverScale = new(1.2f, 1.2f, 1f);
    private readonly Vector3 normalScale = new(0.8f, 0.8f, 1f);

    private readonly int cardsPerRow = 7;

    protected override void Start()
    {
        base.Start();
        InstantiateTarotCards();
    }

    private void InstantiateTarotCards()
    {
        List<MajorArcana> majorArcana = new()
        {
            new Fool(majorArcanaSO),
            new Magician(majorArcanaSO),
            new TheHighPriestess(majorArcanaSO),
            new TheEmpress(majorArcanaSO),
            new TheEmperor(majorArcanaSO),
            new TheHierophant(majorArcanaSO),
            new TheLovers(majorArcanaSO),
            new Chariot(majorArcanaSO),
            new Strength(majorArcanaSO),
            new TheHermit(majorArcanaSO),
            new WheelofFortune(majorArcanaSO),
            new Justice(majorArcanaSO),
            new HangedMan(majorArcanaSO),
            new Temperance(majorArcanaSO),
            new Devil(majorArcanaSO),
            new TheTower(majorArcanaSO),
            new Star(majorArcanaSO),
            new Moon(majorArcanaSO),
            new Sun(majorArcanaSO),
            new Judgement(majorArcanaSO),
            new World(majorArcanaSO),
        };

        foreach (MajorArcana arcana in majorArcana)
        {
            CardContainer cardContainer = Instantiate(cardPrefab, cardGrid);

            cardContainer.SetCard(arcana);
            cardContainer.SetScale(normalScale);
            AddCard(arcana);
            arcana.SetFaceUp(true);
        }
    }

    public void Open()
    {
        foreach(Card card in Cards)
        {
            if (GameManager.Instance.TarotDeck.ContainsTarotCard((MajorArcana)card))
            {
                card.Container.SetColor(Color.white);
            } else
            {
                card.Container.SetColor(Color.gray);
            }
        }
    }

    public void Close()
    {
        foreach(Card card in Cards)
        {
            card.Container.gameObject.SetActive(false);
        }
    }

    public override void UpdateVisuals()
    {
    }

    protected override void EnterContainer(CardContainer container)
    {
        if (container.IsDragging) return;

        container.SetScale(hoverScale);
        container.ShowPopUp(true);
        SoundFXManager.Instance.PlayHoverSoundClip(GameManager.Instance.transform);
    }

    protected override void ExitContainer(CardContainer container)
    {
        if (container.IsDragging) return;

        container.SetScale(normalScale);
        container.ShowPopUp(false);

    }
}