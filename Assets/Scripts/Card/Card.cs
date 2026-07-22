using UnityEngine;

public class Card
{
    public Zone Zone {get; private set;}

    public void SetZone(Zone zone)
    {
        Zone = zone;
    }
}