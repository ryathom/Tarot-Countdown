using UnityEngine;

public class Card
{
    public CardSO CardSO {get; private set;}
    public string Name {get => CardSO.name;}

    public CardContainer Container {get; private set;}
    public Zone Zone {get; private set;}

    public Card(CardSO cardSO)
    {
        this.CardSO = cardSO;
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