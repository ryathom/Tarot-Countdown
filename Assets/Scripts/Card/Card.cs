using UnityEngine;

public abstract class Card
{
    public CardSO CardSO {get; private set;}
    public string Name;

    public int Number;
    public Suit Suit;

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

    public int GetMillCost()
    {
        if (Number > 9) return 3;

        if (Number > 6) return 2;

        if (Number > 3) return 1;
        
        return 0;
    }
}

