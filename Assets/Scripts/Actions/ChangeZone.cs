using System.Collections;
using UnityEngine;

public class ChangeZone : IAction
    {
        public Card Card {get; private set;}
        public Zone EndZone {get; private set;}

        public float Delay {get; private set;}

        public ChangeZone(Card card, Zone endZone, float delay = 0.25f)
        {
            Card = card;
            EndZone = endZone;
            Delay = delay;
        }

        public IEnumerator Execute()
        {
            Card.Zone.RemoveCard(Card);
            EndZone.AddCard(Card);

            yield return new WaitForSeconds(Delay);
        }
    }