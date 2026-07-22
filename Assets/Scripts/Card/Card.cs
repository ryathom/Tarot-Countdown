using UnityEngine;

public class Card
{
    public CardSO CardSO {get; private set;}
    public string Name {get => CardSO.name;}

    public CardContainer Container {get; private set;}
    public Zone Zone {get; private set;}

    public bool FaceUp {get; private set;}

    public Card(CardSO cardSO)
    {
        this.CardSO = cardSO;
    }

    public void SetFaceUp(bool faceup)
    {
        FaceUp = faceup;
        Container.ShowVisual(true);
    }

    public void SetContainer(CardContainer container)
    {
        Container = container;
    }

    public void SetZone(Zone zone)
    {
        Zone = zone;
    }
}