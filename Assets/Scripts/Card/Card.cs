using UnityEngine;

public abstract class Card
{
    public CardSO CardSO {get; private set;}
    public string Name;

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

public class MinorArcana : Card
{
    public int Number;
    public Suit Suit;

    public MinorArcana(CardSO cardSO, int number, Suit suit) : base(cardSO)
    {
        Number = number;
        Suit = suit;
        Name = Number + " of " + Suit;
    }

    
}

public enum Suit
{
    Wands,
    Swords,
    Cups,
    Coins
}

