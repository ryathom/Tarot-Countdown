using System.Collections;
using UnityEngine;

public class ChangeZone : IAction
    {
        public Card Card {get; private set;}
        public Zone EndZone {get; private set;}

        private readonly float delay = 0.25f;

        public ChangeZone(Card card, Zone endZone)
        {
            Card = card;
            EndZone = endZone;
        }

        public IEnumerator Execute()
        {
            Card.Zone.RemoveCard(Card);
            EndZone.AddCard(Card);

            yield return new WaitForSeconds(delay);
        }
    }